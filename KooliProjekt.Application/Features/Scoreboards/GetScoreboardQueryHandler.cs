using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class GetScoreboardQueryHandler : IRequestHandler<GetScoreboardQuery, OperationResult<ScoreboardDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetScoreboardQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult<ScoreboardDto>> Handle(GetScoreboardQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<ScoreboardDto>();
            result.Value = await _dbContext.Scoreboards.AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(scoreboard => new ScoreboardDto
                {
                    Id = scoreboard.Id,
                    TournamentId = scoreboard.TournamentId,
                    UserId = scoreboard.UserId,
                    Score = scoreboard.Score
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
