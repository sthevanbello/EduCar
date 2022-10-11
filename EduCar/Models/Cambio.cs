using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EduCar.Models
{
    public class Cambio
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Tipo é obrigatório")]
        public string Tipo { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ICollection<FichaTecnica> FichasTecnicas { get; set; }
    }
}
