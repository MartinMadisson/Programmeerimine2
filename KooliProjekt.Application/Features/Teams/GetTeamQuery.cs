using MediatR;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
namespace KooliProjekt.Application.Features.Teams
{
    public class GetTeamQuery : IRequest<OperationResult<TeamDto>>
    {
        public int Id { get; set; }
    }
}
