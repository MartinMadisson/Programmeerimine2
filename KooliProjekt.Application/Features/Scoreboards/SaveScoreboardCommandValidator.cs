using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class SaveScoreboardCommandValidator : AbstractValidator<SaveScoreboardCommand>
    {
        public SaveScoreboardCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.Score)
                .GreaterThanOrEqualTo(0).WithMessage("Score must be greater than or equal to 0.");

            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(1).WithMessage("UserId must be greater than or equal to 1.")
                .MustAsync(async (id, ct) => await dbContext.Users.AnyAsync(u => u.id == id, ct))
                .WithMessage("The specified user does not exist.");

            RuleFor(x => x.TournamentId)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(1).WithMessage("TournamentId must be greater than or equal to 1.")
                .MustAsync(async (id, ct) => await dbContext.Tournaments.AnyAsync(t => t.Id == id, ct))
                .WithMessage("The specified tournament does not exist.")
                .MustAsync(async (command, tournamentId, ct) =>
                    !await dbContext.Scoreboards.AnyAsync(s =>
                        s.Id != command.Id && s.TournamentId == tournamentId && s.UserId == command.UserId, ct))
                .WithMessage("This user already has a scoreboard entry in this tournament.");
        }
    }
}
