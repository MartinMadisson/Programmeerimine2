using MediatR;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
namespace KooliProjekt.Application.Features.Matches
{
    public class GetMatchQuery : IRequest<OperationResult<MatchDto>>
    {
        public int Id { get; set; }
    }
}
