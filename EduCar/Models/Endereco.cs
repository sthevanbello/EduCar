using System.ComponentModel.DataAnnotations;

namespace EduCar.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Logradouro é obrigatório")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatório")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        [StringLength(2, ErrorMessage = "O Estado deve conter apenas dois caracteres")]        
        public string Estado { get; set; }

        [Required(ErrorMessage = "Número é obrigatório")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório")]
        [StringLength(8, ErrorMessage = "O CEP deve conter apenas 8 caracteres (apenas números)")]
        public string CEP { get; set; }
    }
}
