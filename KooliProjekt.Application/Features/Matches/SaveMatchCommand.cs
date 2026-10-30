using System;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Matches
{
    public class SaveMatchCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TournamentId { get; set; }
        public int Teamt1Id { get; set; }
        public int Team2Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public int RoundId { get; set; }
    }
}
