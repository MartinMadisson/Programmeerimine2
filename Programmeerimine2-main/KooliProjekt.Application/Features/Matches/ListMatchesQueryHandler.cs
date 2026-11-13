using KooliProjekt.Application.Data.Football_prediction_MartinM;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Matches
{
    public class ListMatchesQueryHandler : IRequestHandler<ListMatchesQuery, IEnumerable<Match>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListMatchesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Match>> Handle(ListMatchesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Matches.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
