using KooliProjekt.Application.Data.Football_prediction_MartinM;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class ListScoreboardsQueryHandler : IRequestHandler<ListScoreboardsQuery, IEnumerable<Scoreboard>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListScoreboardsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Scoreboard>> Handle(ListScoreboardsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Scoreboards.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
