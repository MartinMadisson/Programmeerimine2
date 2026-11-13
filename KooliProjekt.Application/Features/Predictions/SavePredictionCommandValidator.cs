using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Predictions
{
    public class SavePredictionCommandValidator : AbstractValidator<SavePredictionCommand>
    {
        private const int MaxScore = 20;   // "Prediction peab olema reaalne"
        private const int MaxPoints = 10;

        public SavePredictionCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.Team1Score)
                .InclusiveBetween(0, MaxScore).WithMessage($"Team1Score must be between 0 and {MaxScore}.");

            RuleFor(x => x.Team2Score)
                .InclusiveBetween(0, MaxScore).WithMessage($"Team2Score must be between 0 and {MaxScore}.");

            RuleFor(x => x.Points)
                .InclusiveBetween(0, MaxPoints).WithMessage($"Points must be between 0 and {MaxPoints}.");

            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(1).WithMessage("UserId must be greater than or equal to 1.")
                .MustAsync(async (id, ct) => await dbContext.Users.AnyAsync(u => u.id == id, ct))
                .WithMessage("The specified user does not exist.");

            RuleFor(x => x.MatchId)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(1).WithMessage("MatchId must be greater than or equal to 1.")
                .MustAsync(async (id, ct) => await dbContext.Matches.AnyAsync(m => m.Id == id, ct))
                .WithMessage("The specified match does not exist.")
                .MustAsync(async (command, matchId, ct) =>
                    !await dbContext.Predictions.AnyAsync(p =>
                        p.Id != command.Id && p.MatchId == matchId && p.UserId == command.UserId, ct))
                .WithMessage("This user has already made a prediction for this match.");
        }
    }
}
