using System;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class SaveScoreboardCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
    }
}
