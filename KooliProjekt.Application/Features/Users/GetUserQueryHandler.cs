using System.Linq;
using KooliProjekt.Application.Data.Football_prediction_MartinM;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserSummary>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetUserQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserSummary> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AsNoTracking().Where(u => u.id == request.Id).Select(u => new UserSummary { Id = u.id, Username = u.username }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
