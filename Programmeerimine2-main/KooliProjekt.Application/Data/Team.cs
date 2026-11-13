using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

public class Team
{
[Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    public int Id { get; set; }
}
