using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.Matches
{
    public class ListMatchesQueryHandler : IRequestHandler<ListMatchesQuery, OperationResult<PagedResult<MatchItemDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListMatchesQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }
        public async Task<OperationResult<PagedResult<MatchItemDto>>> Handle(ListMatchesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<MatchItemDto>>();
            result.Value = await _dbContext.Matches.AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(match => new MatchItemDto
                {
                    Id = match.Id,
                    Date = match.Date,
                    TournamentId = match.TournamentId,
                    Teamt1Id = match.Teamt1Id,
                    Team2Id = match.Team2Id,
                    Team1Score = match.Team1Score,
                    Team2Score = match.Team2Score,
                    RoundId = match.RoundId
                })
                .GetPagedAsync(request.Page, request.PageSize);
            return result;
        }
    }
}
