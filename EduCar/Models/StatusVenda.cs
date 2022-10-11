using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduCar.Models
{
    public class StatusVenda
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        public string Status { get; set; }

        public ICollection<Veiculo> Veiculos { get; set; }
    }
}
