using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.Players
{
    public class ListPlayersQueryHandler : IRequestHandler<ListPlayersQuery, OperationResult<PagedResult<PlayerItemDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListPlayersQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }
        public async Task<OperationResult<PagedResult<PlayerItemDto>>> Handle(ListPlayersQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<PlayerItemDto>>();
            result.Value = await _dbContext.Players.AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(player => new PlayerItemDto
                {
                    Id = player.Id,
                    Name = player.Name
                })
                .GetPagedAsync(request.Page, request.PageSize);
            return result;
        }
    }
}
