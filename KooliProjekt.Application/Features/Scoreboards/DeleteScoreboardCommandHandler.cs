using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class DeleteScoreboardCommandHandler : IRequestHandler<DeleteScoreboardCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteScoreboardCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(DeleteScoreboardCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var item = await _dbContext.Scoreboards.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null) return result.AddPropertyError("Id", "Scoreboard not found");

            _dbContext.Scoreboards.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
