using KooliProjekt.Application.Data.Football_prediction_MartinM;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Matches
{
    public class GetMatchQueryHandler : IRequestHandler<GetMatchQuery, Match>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetMatchQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Match> Handle(GetMatchQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Matches.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
