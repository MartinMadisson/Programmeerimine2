using System.Collections.Generic;
using MediatR;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class ListScoreboardsQuery : IRequest<IEnumerable<Scoreboard>>
    {
    }
}
