﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduCar.Models
{
    public class Cambio
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Tipo é obrigatório")]
        public string Tipo { get; set; }
        public ICollection<FichaTecnica> FichasTecnicas { get; set; }
    }
}
