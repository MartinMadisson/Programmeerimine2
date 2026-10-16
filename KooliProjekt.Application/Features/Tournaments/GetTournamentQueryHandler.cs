using KooliProjekt.Application.Data.Football_prediction_MartinM;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Tournaments
{
    public class GetTournamentQueryHandler : IRequestHandler<GetTournamentQuery, Tournament>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetTournamentQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tournament> Handle(GetTournamentQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Tournaments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
