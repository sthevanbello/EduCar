using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCar.Models
{
    public class FichaTecnica
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Modelo do veículo é obrigatório")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Ano do veículo é obrigatório")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Consumo do veículo é obrigatório")]
        public double Consumo { get; set; }

        [Required(ErrorMessage = "Quilometragem do veículo é obrigatória")]
        public string Quilometragem { get; set; }

        //[ForeignKey("Direcao")]
        //public int IdDirecao { get; set; }
        //public Direcao Direcao { get; set; }

        //[ForeignKey("Cambio")]
        //public int IdCambio { get; set; }
        //public Cambio Cambio { get; set; }
    }
}
