using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Users
{
    public class SaveUserCommandValidator : AbstractValidator<SaveUserCommand>
    {
        public SaveUserCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            // Kasutajanimi: kohustuslik, unikaalne (suurtähtedest sõltumatult), max 50 (andmebaasi piirang)
            RuleFor(x => x.username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
                .MustAsync(async (command, name, ct) =>
                    !await dbContext.Users.AnyAsync(u =>
                        u.id != command.Id && u.username.ToLower() == name.Trim().ToLower(), ct))
                .WithMessage("A user with the same or a similar username already exists.");

            // NB! Paroolid salvestatakse räsina (iga räsi on soolatud ja erinev), seega
            // "parool peab olema unikaalne" reeglit ei saa enam andmebaasist kontrollida.
            RuleFor(x => x.password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(100).WithMessage("Password must not exceed 100 characters.")
                .Must((command, password) =>
                    !string.Equals(command.username?.Trim(), password.Trim(), System.StringComparison.OrdinalIgnoreCase))
                .WithMessage("Password must not be the same as the username.")
                .MustAsync(async (command, password, ct) =>
                    !await dbContext.Users.AnyAsync(u =>
                        u.id != command.Id && u.username.ToLower() == password.Trim().ToLower(), ct))
                .WithMessage("Password must not be the same as another user's username.");
        }
    }
}
