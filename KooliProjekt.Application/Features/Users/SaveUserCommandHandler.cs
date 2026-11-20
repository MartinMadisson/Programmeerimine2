using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Authentication;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Users
{
    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        public SaveUserCommandHandler(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        public async Task<OperationResult> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            User item;
            if (request.Id == 0)
            {
                item = new User();
                _dbContext.Users.Add(item);
            }
            else
            {
                item = await _dbContext.Users.FirstOrDefaultAsync(x => x.id == request.Id, cancellationToken);
                if (item == null) return result.AddPropertyError("Id", "User not found");
            }

            item.username = request.username;
            // Parooli ei hoita kunagi tavatekstina, andmebaasi läheb räsi
            item.password = PasswordHelper.Hash(request.password);
            item.IsAdmin = request.IsAdmin;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
