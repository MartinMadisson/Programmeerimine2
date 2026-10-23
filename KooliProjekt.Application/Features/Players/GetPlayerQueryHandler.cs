using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Players
{
    public class GetPlayerQueryHandler : IRequestHandler<GetPlayerQuery, OperationResult<PlayerDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetPlayerQueryHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult<PlayerDto>> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PlayerDto>();
            result.Value = await _dbContext.Players.AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(player => new PlayerDto
                {
                    Id = player.Id,
                    Name = player.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
