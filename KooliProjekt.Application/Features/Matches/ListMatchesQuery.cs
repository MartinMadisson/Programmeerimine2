using MediatR;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
namespace KooliProjekt.Application.Features.Matches
{
    public class ListMatchesQuery : IRequest<OperationResult<PagedResult<MatchItemDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
