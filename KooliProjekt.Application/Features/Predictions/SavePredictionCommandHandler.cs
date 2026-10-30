using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Predictions
{
    public class SavePredictionCommandHandler : IRequestHandler<SavePredictionCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public SavePredictionCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(SavePredictionCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            if (!await _dbContext.Matches.AnyAsync(x => x.Id == request.MatchId, cancellationToken))
                return result.AddPropertyError("MatchId", "Match not found");
            if (!await _dbContext.Users.AnyAsync(x => x.id == request.UserId, cancellationToken))
                return result.AddPropertyError("UserId", "User not found");

            Prediction item;
            if (request.Id == 0)
            {
                item = new Prediction();
                _dbContext.Predictions.Add(item);
            }
            else
            {
                item = await _dbContext.Predictions.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (item == null) return result.AddPropertyError("Id", "Prediction not found");
            }

            item.Team1Score = request.Team1Score;
            item.Team2Score = request.Team2Score;
            item.Points = request.Points;
            item.MatchId = request.MatchId;
            item.UserId = request.UserId;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
