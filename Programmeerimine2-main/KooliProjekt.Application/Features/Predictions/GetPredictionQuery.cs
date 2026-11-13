using MediatR;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Predictions
{
    public class GetPredictionQuery : IRequest<Prediction>
    {
        public int Id { get; set; }
    }
}
