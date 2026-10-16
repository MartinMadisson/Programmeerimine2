using System.Collections.Generic;
using MediatR;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Tournaments
{
    public class ListTournamentsQuery : IRequest<IEnumerable<Tournament>>
    {
    }
}
