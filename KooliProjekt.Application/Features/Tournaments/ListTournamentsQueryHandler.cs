using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.Tournaments
{
    public class ListTournamentsQueryHandler : IRequestHandler<ListTournamentsQuery, OperationResult<PagedResult<TournamentItemDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListTournamentsQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }
        public async Task<OperationResult<PagedResult<TournamentItemDto>>> Handle(ListTournamentsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<TournamentItemDto>>();
            result.Value = await _dbContext.Tournaments.AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(tournament => new TournamentItemDto
                {
                    Id = tournament.Id,
                    Name = tournament.Name
                })
                .GetPagedAsync(request.Page, request.PageSize);
            return result;
        }
    }
}
