using MediatR;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
namespace KooliProjekt.Application.Features.Players
{
    public class GetPlayerQuery : IRequest<OperationResult<PlayerDto>>
    {
        public int Id { get; set; }
    }
}
