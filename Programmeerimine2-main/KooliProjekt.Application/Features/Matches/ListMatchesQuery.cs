using System.Collections.Generic;
using MediatR;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Matches
{
    public class ListMatchesQuery : IRequest<IEnumerable<Match>>
    {
    }
}
