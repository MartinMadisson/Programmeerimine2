using System.Collections.Generic;
using MediatR;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Predictions
{
    public class ListPredictionsQuery : IRequest<IEnumerable<Prediction>>
    {
    }
}
