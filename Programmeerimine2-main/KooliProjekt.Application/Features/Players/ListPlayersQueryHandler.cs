using KooliProjekt.Application.Data.Football_prediction_MartinM;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Players
{
    public class ListPlayersQueryHandler : IRequestHandler<ListPlayersQuery, IEnumerable<Player>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListPlayersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Player>> Handle(ListPlayersQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Players.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
