using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.Users
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, OperationResult<PagedResult<UserItemDto>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListUsersQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }
        public async Task<OperationResult<PagedResult<UserItemDto>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<UserItemDto>>();
            result.Value = await _dbContext.Users.AsNoTracking()
                .OrderByDescending(x => x.id)
                .Select(user => new UserItemDto
                {
                    Id = user.id,
                    Username = user.username
                })
                .GetPagedAsync(request.Page, request.PageSize);
            return result;
        }
    }
}
