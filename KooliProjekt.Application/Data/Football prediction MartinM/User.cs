using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KooliProjekt.Application.Data.Football_prediction_MartinM
{
    public class User
    {
        public int id { get; set; }


        [Required]
        [MaxLength(50)]
        public string username { get; set; }

        [Required]
        [MaxLength(100)]
        public string password { get; set; }


    }
    }
