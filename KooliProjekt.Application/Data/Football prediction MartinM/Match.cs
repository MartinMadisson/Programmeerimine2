using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data.Football_prediction_MartinM
{
    public class  Match
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public int TournamentId { get; set; }
        public int Teamt1Id { get; set; }
        public int Team2Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public int RoundId { get; set; }
    }
}
