using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCar.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("Usuario")]
        //public int IdUsuario { get; set; }
        //public Usuario Usuario { get; set; }

        //[ForeignKey("Concessionaria")]
        //public int IdConcessionaria { get; set; }
        //public Concessionaria Concessionaria { get; set; }

        //[ForeignKey("Veiculo")]
        //public int IdVeiculo { get; set; }
        //public Veiculo Veiculo { get; set; }
    }
}
