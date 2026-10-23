using MediatR;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
namespace KooliProjekt.Application.Features.Tournaments
{
    public class ListTournamentsQuery : IRequest<OperationResult<PagedResult<TournamentItemDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
