using MediatR;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
namespace KooliProjekt.Application.Features.Scoreboards
{
    public class GetScoreboardQuery : IRequest<OperationResult<ScoreboardDto>>
    {
        public int Id { get; set; }
    }
}
