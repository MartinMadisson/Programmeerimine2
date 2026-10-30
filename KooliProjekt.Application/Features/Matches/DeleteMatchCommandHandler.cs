using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Matches
{
    public class DeleteMatchCommandHandler : IRequestHandler<DeleteMatchCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteMatchCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var item = await _dbContext.Matches.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null) return result.AddPropertyError("Id", "Match not found");

            _dbContext.Matches.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
