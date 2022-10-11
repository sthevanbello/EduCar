using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EduCar.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Usuario Usuario { get; set; }

        [ForeignKey("Concessionaria")]
        public int IdConcessionaria { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Concessionaria Concessionaria { get; set; }

        [ForeignKey("Veiculo")]
        public int IdVeiculo { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Veiculo Veiculo { get; set; }

        [ForeignKey("Cartao")]
        public int IdCartao { get; set; }
        public Cartao Cartao { get; set; }
        [NotMapped]
        public bool SalvarCartaoNoBanco { get; set; }
    }
}
