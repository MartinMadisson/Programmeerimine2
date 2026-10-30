using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Players
{
    public class DeletePlayerCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
    }
}
