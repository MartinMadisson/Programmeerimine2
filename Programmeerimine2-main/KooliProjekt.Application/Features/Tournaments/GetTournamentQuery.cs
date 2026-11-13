using MediatR;
using KooliProjekt.Application.Data.Football_prediction_MartinM;

namespace KooliProjekt.Application.Features.Tournaments
{
    public class GetTournamentQuery : IRequest<Tournament>
    {
        public int Id { get; set; }
    }
}
