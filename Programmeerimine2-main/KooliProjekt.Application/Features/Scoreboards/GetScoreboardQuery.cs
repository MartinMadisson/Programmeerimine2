using MediatR;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class GetScoreboardQuery : IRequest<Scoreboard>
    {
        public int Id { get; set; }
    }
}
