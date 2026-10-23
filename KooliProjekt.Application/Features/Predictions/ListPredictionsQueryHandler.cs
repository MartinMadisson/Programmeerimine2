using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.Predictions
{
    public class ListPredictionsQueryHandler : IRequestHandler<ListPredictionsQuery, OperationResult<PagedResult<PredictionItemDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListPredictionsQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }
        public async Task<OperationResult<PagedResult<PredictionItemDto>>> Handle(ListPredictionsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<PredictionItemDto>>();
            result.Value = await _dbContext.Predictions.AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(prediction => new PredictionItemDto
                {
                    Id = prediction.Id,
                    Team1Score = prediction.Team1Score,
                    Team2Score = prediction.Team2Score,
                    Points = prediction.Points,
                    MatchId = prediction.MatchId,
                    UserId = prediction.UserId
                })
                .GetPagedAsync(request.Page, request.PageSize);
            return result;
        }
    }
}
