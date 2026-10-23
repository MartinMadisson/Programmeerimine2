using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.Teams
{
    public class ListTeamsQueryHandler : IRequestHandler<ListTeamsQuery, OperationResult<PagedResult<TeamItemDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListTeamsQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }
        public async Task<OperationResult<PagedResult<TeamItemDto>>> Handle(ListTeamsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<TeamItemDto>>();
            result.Value = await _dbContext.Teams.AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(team => new TeamItemDto
                {
                    Id = team.Id,
                    Name = team.Name
                })
                .GetPagedAsync(request.Page, request.PageSize);
            return result;
        }
    }
}
