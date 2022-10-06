using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCar.Models
{
    public class Concessionaria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da Concessionária é obrigatório")]        
        public string Nome { get; set; }

        [ForeignKey("Endereco")]
        public int IdEndereco { get; set; }
        public Endereco Endereco { get; set; }

        //public ICollection<Veiculos> Veiculos{ get; set; }
        public ICollection<Pedido> Pedido { get; set; }


    }
}
