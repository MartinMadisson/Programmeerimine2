using MediatR;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
namespace KooliProjekt.Application.Features.Teams
{
    public class ListTeamsQuery : IRequest<OperationResult<PagedResult<TeamItemDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
