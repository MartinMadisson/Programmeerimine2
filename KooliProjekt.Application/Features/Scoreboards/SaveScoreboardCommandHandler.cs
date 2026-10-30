using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class SaveScoreboardCommandHandler : IRequestHandler<SaveScoreboardCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public SaveScoreboardCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(SaveScoreboardCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            if (!await _dbContext.Tournaments.AnyAsync(x => x.Id == request.TournamentId, cancellationToken))
                return result.AddPropertyError("TournamentId", "Tournament not found");
            if (!await _dbContext.Users.AnyAsync(x => x.id == request.UserId, cancellationToken))
                return result.AddPropertyError("UserId", "User not found");

            Scoreboard item;
            if (request.Id == 0)
            {
                item = new Scoreboard();
                _dbContext.Scoreboards.Add(item);
            }
            else
            {
                item = await _dbContext.Scoreboards.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (item == null) return result.AddPropertyError("Id", "Scoreboard not found");
            }

            item.TournamentId = request.TournamentId;
            item.UserId = request.UserId;
            item.Score = request.Score;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
