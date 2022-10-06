using System.ComponentModel.DataAnnotations;

namespace EduCar.Models
{
    public class TipoUsuario
    {
       
            // Na classe Model, haverá todos os atributos/colunas que compõe a classe TipoUsuario

            [Key]   // primary key 
            public int Id { get; set; }

            [Required(ErrorMessage ="Tipo é obrigatório")]  // campo obrigatório
            public string Tipo { get; set; }
        }
 

}
