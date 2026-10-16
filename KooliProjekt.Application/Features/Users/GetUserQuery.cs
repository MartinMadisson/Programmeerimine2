using MediatR;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Users
{
    public class GetUserQuery : IRequest<UserSummary>
    {
        public int Id { get; set; }
    }
}
