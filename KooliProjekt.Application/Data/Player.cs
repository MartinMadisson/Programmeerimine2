using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data.Football_prediction_MartinM
{
    public class Player
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Additional properties can be added as needed
    }
}
