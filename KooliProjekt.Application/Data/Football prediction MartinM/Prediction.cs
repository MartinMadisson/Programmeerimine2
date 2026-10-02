using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data.Football_prediction_MartinM
{
    public class Prediction
    {
        public int Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public int Points { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }
    }
}
