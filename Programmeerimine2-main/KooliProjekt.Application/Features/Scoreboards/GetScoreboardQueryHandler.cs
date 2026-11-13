using KooliProjekt.Application.Data.Football_prediction_MartinM;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class GetScoreboardQueryHandler : IRequestHandler<GetScoreboardQuery, Scoreboard>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetScoreboardQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Scoreboard> Handle(GetScoreboardQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Scoreboards.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
