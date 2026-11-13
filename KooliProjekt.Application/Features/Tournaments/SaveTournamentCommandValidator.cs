using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Tournaments
{
    public class SaveTournamentCommandValidator : AbstractValidator<SaveTournamentCommand>
    {
        public SaveTournamentCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .MustAsync(async (command, name, ct) =>
                    !await dbContext.Tournaments.AnyAsync(x => x.Name == name && x.Id != command.Id, ct))
                .WithMessage("A tournament with the same name already exists.");
        }
    }
}
