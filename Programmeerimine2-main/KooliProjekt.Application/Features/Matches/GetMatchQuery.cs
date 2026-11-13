using MediatR;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Matches
{
    public class GetMatchQuery : IRequest<Match>
    {
        public int Id { get; set; }
    }
}
