using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Predictions
{
    public class DeletePredictionCommandHandler : IRequestHandler<DeletePredictionCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeletePredictionCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(DeletePredictionCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var item = await _dbContext.Predictions.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null) return result.AddPropertyError("Id", "Prediction not found");

            _dbContext.Predictions.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
