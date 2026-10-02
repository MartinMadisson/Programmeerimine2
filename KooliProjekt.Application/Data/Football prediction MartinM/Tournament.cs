using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KooliProjekt.Application.Data.Football_prediction_MartinM
{

      public class Tournament
        {
        [Required]
            public int Id { get; set; }

            [Required]
            [StringLength(100)]
            public string Name { get; set; }
    }
