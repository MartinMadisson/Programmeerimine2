using MediatR;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
namespace KooliProjekt.Application.Features.Predictions
{
    public class ListPredictionsQuery : IRequest<OperationResult<PagedResult<PredictionItemDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
