using KooliProjekt.Application.Data.Football_prediction_MartinM;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Tournaments
{
    public class ListTournamentsQueryHandler : IRequestHandler<ListTournamentsQuery, IEnumerable<Tournament>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListTournamentsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tournament>> Handle(ListTournamentsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Tournaments.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
