using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EduCar.Models
{
    public class Concessionaria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da Concessionária é obrigatório")]        
        public string Nome { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(10)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Site é obrigatório")]
        public string Site { get; set; }


        [ForeignKey("Endereco")]
        public int IdEndereco { get; set; }
        public Endereco Endereco { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ICollection<Veiculo> Veiculos { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ICollection<Pedido> Pedidos { get; set; }


    }
}
