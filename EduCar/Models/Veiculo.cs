using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EduCar.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Valor é obrigatório")]
        public double Valor { get; set; }
        
        [ForeignKey("StatusVenda")]   // foreign key/ chave estrangeira IdStatusVenda        
        public int IdStatusVenda { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public StatusVenda StatusVenda { get; set; }   // classe StatusVenda como objeto

        [ForeignKey("Concessionaria")]   // foreign key/ chave estrangeira IdConcessionaria        
        public int IdConcessionaria { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Concessionaria Concessionaria { get; set; }   // classe Concessionaria como objeto

        [ForeignKey("FichaTecnica")]   // foreign key/ chave estrangeira IdFichaTecnica        
        public int IdFichaTecnica { get; set; }
        public FichaTecnica FichaTecnica { get; set; }

        [ForeignKey("CaracteristicasGerais")]   // foreign key/ chave estrangeira IdFichaTecnica        
        public int IdCaracteristicasGerais { get; set; }
        public CaracteristicasGerais CaracteristicasGerais { get; set; }


    }
}
