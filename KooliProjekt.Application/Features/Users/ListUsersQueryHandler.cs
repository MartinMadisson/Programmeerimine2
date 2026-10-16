using System.Linq;
using KooliProjekt.Application.Data.Football_prediction_MartinM;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Users
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, IEnumerable<UserSummary>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListUsersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserSummary>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AsNoTracking().Select(u => new UserSummary { Id = u.id, Username = u.username }).ToListAsync(cancellationToken);
        }
    }
}
