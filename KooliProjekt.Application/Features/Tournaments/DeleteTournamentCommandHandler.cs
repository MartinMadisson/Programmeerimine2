using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Tournaments
{
    public class DeleteTournamentCommandHandler : IRequestHandler<DeleteTournamentCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteTournamentCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(DeleteTournamentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var item = await _dbContext.Tournaments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null) return result.AddPropertyError("Id", "Tournament not found");

            _dbContext.Tournaments.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
