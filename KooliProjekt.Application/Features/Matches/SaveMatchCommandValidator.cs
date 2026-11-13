using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Matches
{
    public class SaveMatchCommandValidator : AbstractValidator<SaveMatchCommand>
    {
        private const int MaxScore = 30;   // "reaalne" tulemus
        private const int MaxRound = 4;    // README: Round of 16, Quarter-final, Semi-final, Final

        public SaveMatchCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.");

            RuleFor(x => x.TournamentId)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(1).WithMessage("TournamentId must be greater than or equal to 1.")
                .MustAsync(async (id, ct) => await dbContext.Tournaments.AnyAsync(t => t.Id == id, ct))
                .WithMessage("The specified tournament does not exist.");

            RuleFor(x => x.Teamt1Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(1).WithMessage("Teamt1Id must be greater than or equal to 1.")
                .MustAsync(async (id, ct) => await dbContext.Teams.AnyAsync(t => t.Id == id, ct))
                .WithMessage("The specified team 1 does not exist.");

            RuleFor(x => x.Team2Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(1).WithMessage("Team2Id must be greater than or equal to 1.")
                .Must((command, team2Id) => team2Id != command.Teamt1Id)
                .WithMessage("Team 1 and team 2 must be different teams.")
                .MustAsync(async (id, ct) => await dbContext.Teams.AnyAsync(t => t.Id == id, ct))
                .WithMessage("The specified team 2 does not exist.");

            RuleFor(x => x.Team1Score)
                .InclusiveBetween(0, MaxScore).WithMessage($"Team1Score must be between 0 and {MaxScore}.");

            RuleFor(x => x.Team2Score)
                .InclusiveBetween(0, MaxScore).WithMessage($"Team2Score must be between 0 and {MaxScore}.");

            RuleFor(x => x.RoundId)
                .Cascade(CascadeMode.Stop)
                .InclusiveBetween(1, MaxRound).WithMessage($"RoundId must be between 1 and {MaxRound}.")
                .MustAsync(async (command, round, ct) =>
                    !await dbContext.Matches.AnyAsync(m =>
                        m.Id != command.Id &&
                        m.TournamentId == command.TournamentId &&
                        m.RoundId == round &&
                        ((m.Teamt1Id == command.Teamt1Id && m.Team2Id == command.Team2Id) ||
                         (m.Teamt1Id == command.Team2Id && m.Team2Id == command.Teamt1Id)), ct))
                .WithMessage("The same two teams already play each other in this round of this tournament.");
        }
    }
}
