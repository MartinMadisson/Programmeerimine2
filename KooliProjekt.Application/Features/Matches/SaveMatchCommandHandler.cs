using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Matches
{
    public class SaveMatchCommandHandler : IRequestHandler<SaveMatchCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public SaveMatchCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(SaveMatchCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            if (!await _dbContext.Tournaments.AnyAsync(x => x.Id == request.TournamentId, cancellationToken))
                return result.AddPropertyError("TournamentId", "Tournament not found");
            if (!await _dbContext.Teams.AnyAsync(x => x.Id == request.Teamt1Id, cancellationToken))
                return result.AddPropertyError("Teamt1Id", "Team not found");
            if (!await _dbContext.Teams.AnyAsync(x => x.Id == request.Team2Id, cancellationToken))
                return result.AddPropertyError("Team2Id", "Team not found");

            Match item;
            if (request.Id == 0)
            {
                item = new Match();
                _dbContext.Matches.Add(item);
            }
            else
            {
                item = await _dbContext.Matches.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (item == null) return result.AddPropertyError("Id", "Match not found");
            }

            item.Date = request.Date;
            item.TournamentId = request.TournamentId;
            item.Teamt1Id = request.Teamt1Id;
            item.Team2Id = request.Team2Id;
            item.Team1Score = request.Team1Score;
            item.Team2Score = request.Team2Score;
            item.RoundId = request.RoundId;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
