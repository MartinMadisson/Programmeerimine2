using MediatR;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
namespace KooliProjekt.Application.Features.Tournaments
{
    public class GetTournamentQuery : IRequest<OperationResult<TournamentDto>>
    {
        public int Id { get; set; }
    }
}
