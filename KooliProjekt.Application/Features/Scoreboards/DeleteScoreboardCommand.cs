using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class DeleteScoreboardCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
    }
}
