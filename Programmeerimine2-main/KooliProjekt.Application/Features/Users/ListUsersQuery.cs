using System.Collections.Generic;
using MediatR;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Users
{
    public class ListUsersQuery : IRequest<IEnumerable<UserSummary>>
    {
    }
}
