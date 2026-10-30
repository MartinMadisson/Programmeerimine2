using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Players
{
    public class SavePlayerCommandHandler : IRequestHandler<SavePlayerCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public SavePlayerCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(SavePlayerCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            Player item;
            if (request.Id == 0)
            {
                item = new Player();
                _dbContext.Players.Add(item);
            }
            else
            {
                item = await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (item == null) return result.AddPropertyError("Id", "Player not found");
            }

            item.Name = request.Name;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
