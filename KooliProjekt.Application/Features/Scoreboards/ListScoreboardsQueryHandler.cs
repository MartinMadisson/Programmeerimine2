using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.Scoreboards
{
    public class ListScoreboardsQueryHandler : IRequestHandler<ListScoreboardsQuery, OperationResult<PagedResult<ScoreboardItemDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListScoreboardsQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }
        public async Task<OperationResult<PagedResult<ScoreboardItemDto>>> Handle(ListScoreboardsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<ScoreboardItemDto>>();
            result.Value = await _dbContext.Scoreboards.AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(scoreboard => new ScoreboardItemDto
                {
                    Id = scoreboard.Id,
                    TournamentId = scoreboard.TournamentId,
                    UserId = scoreboard.UserId,
                    Score = scoreboard.Score
                })
                .GetPagedAsync(request.Page, request.PageSize);
            return result;
        }
    }
}
