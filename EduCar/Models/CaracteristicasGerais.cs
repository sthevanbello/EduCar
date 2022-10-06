using System.ComponentModel.DataAnnotations;

namespace EduCar.Models
{
    public class CaracteristicasGerais
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Marca é obrigatória")]
        public string Marca { get; set; }
        [Required(ErrorMessage = "Placa é obrigatória")]
        public double Placa { get; set; }
        [Required(ErrorMessage = "Cor é obrigatória")]
        public string Cor { get; set; }
        [Required(ErrorMessage = "Número de assentos é obrigatório")]
        public int Assentos { get; set; }
        [Required(ErrorMessage = "Número de portas é obrigatório")]
        public string Portas { get; set; }
    }
}
