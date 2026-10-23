using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Matches
{
    public class GetMatchQueryHandler : IRequestHandler<GetMatchQuery, OperationResult<MatchDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetMatchQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult<MatchDto>> Handle(GetMatchQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<MatchDto>();
            result.Value = await _dbContext.Matches.AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(match => new MatchDto
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
                .FirstOrDefaultAsync(cancellationToken);
            if (result.Value != null)
            {
                result.Value.Predictions = await _dbContext.Predictions.AsNoTracking()
                    .Where(p => p.MatchId == result.Value.Id)
                    .OrderBy(p => p.Id)
                    .Select(p => new PredictionDto { Id = p.Id, Team1Score = p.Team1Score, Team2Score = p.Team2Score, Points = p.Points, MatchId = p.MatchId, UserId = p.UserId })
                    .ToListAsync(cancellationToken);
            }
            return result;
        }
    }
}
