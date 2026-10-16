using System.Collections.Generic;
using MediatR;

namespace KooliProjekt.Application.Features.Teams
{
    public class ListTeamsQuery : IRequest<IEnumerable<Team>>
    {
    }
}
