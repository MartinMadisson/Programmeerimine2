using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KooliProjekt.Application.Data.Football_prediction_MartinM
{
    public class Prediction
    {
        public int Id { get; set; }
        [Required]
        public int Team1Score { get; set; }
        [Required]
        public int Team2Score { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public int MatchId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
