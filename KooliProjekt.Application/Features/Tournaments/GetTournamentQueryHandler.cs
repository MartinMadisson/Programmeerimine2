using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Tournaments
{
    public class GetTournamentQueryHandler : IRequestHandler<GetTournamentQuery, OperationResult<TournamentDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetTournamentQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult<TournamentDto>> Handle(GetTournamentQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<TournamentDto>();
            result.Value = await _dbContext.Tournaments.AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(tournament => new TournamentDto
                {
                    Id = tournament.Id,
                    Name = tournament.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
