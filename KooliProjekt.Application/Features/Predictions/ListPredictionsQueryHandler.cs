using KooliProjekt.Application.Data.Football_prediction_MartinM;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Predictions
{
    public class ListPredictionsQueryHandler : IRequestHandler<ListPredictionsQuery, IEnumerable<Prediction>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListPredictionsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Prediction>> Handle(ListPredictionsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Predictions.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
