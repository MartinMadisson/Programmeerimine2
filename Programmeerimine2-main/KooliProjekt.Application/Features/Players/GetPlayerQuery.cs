using MediatR;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Players
{
    public class GetPlayerQuery : IRequest<Player>
    {
        public int Id { get; set; }
    }
}
