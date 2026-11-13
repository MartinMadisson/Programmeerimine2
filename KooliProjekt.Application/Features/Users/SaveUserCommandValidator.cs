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

            // Klassidokumentatsioon: nimi ja parool unikaalsed, mitte sarnased teiste kasutajate nimedega.
            // Andmebaasis on username veerg 50 märki (User.cs), seetõttu piirame selle 50-ga.
            RuleFor(x => x.username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
                .MustAsync(async (command, name, ct) =>
                    !await dbContext.Users.AnyAsync(u =>
                        u.id != command.Id && u.username.ToLower() == name.Trim().ToLower(), ct))
                .WithMessage("A user with the same or a similar username already exists.")
                .MustAsync(async (command, name, ct) =>
                    !await dbContext.Users.AnyAsync(u =>
                        u.id != command.Id && u.password.ToLower() == name.Trim().ToLower(), ct))
                .WithMessage("Username must not be the same as another user's password.");

            RuleFor(x => x.password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(100).WithMessage("Password must not exceed 100 characters.")
                .Must((command, password) =>
                    !string.Equals(command.username?.Trim(), password.Trim(), System.StringComparison.OrdinalIgnoreCase))
                .WithMessage("Password must not be the same as the username.")
                .MustAsync(async (command, password, ct) =>
                    !await dbContext.Users.AnyAsync(u => u.id != command.Id && u.password == password, ct))
                .WithMessage("This password is already in use by another user.")
                .MustAsync(async (command, password, ct) =>
                    !await dbContext.Users.AnyAsync(u =>
                        u.id != command.Id && u.username.ToLower() == password.Trim().ToLower(), ct))
                .WithMessage("Password must not be the same as another user's username.");
        }
    }
}
