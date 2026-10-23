using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Predictions
{
    public class GetPredictionQueryHandler : IRequestHandler<GetPredictionQuery, OperationResult<PredictionDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetPredictionQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult<PredictionDto>> Handle(GetPredictionQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PredictionDto>();
            result.Value = await _dbContext.Predictions.AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(prediction => new PredictionDto
                {
                    Id = prediction.Id,
                    Team1Score = prediction.Team1Score,
                    Team2Score = prediction.Team2Score,
                    Points = prediction.Points,
                    MatchId = prediction.MatchId,
                    UserId = prediction.UserId
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
