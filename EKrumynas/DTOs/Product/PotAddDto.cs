﻿using System.ComponentModel.DataAnnotations;

namespace EKrumynas.DTOs
{
    public class PotAddDto
    {
        [Required] public string Color { get; set; }
        [Required] public string Size { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public int Stock { get; set; }
    }
}
