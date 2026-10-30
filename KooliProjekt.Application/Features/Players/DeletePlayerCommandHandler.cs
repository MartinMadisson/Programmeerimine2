using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Players
{
    public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeletePlayerCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var item = await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null) return result.AddPropertyError("Id", "Player not found");

            _dbContext.Players.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
