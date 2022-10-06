using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCar.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome é obrigatório")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "CPF/CNPJ é obrigatório")]
        [MinLength(11, ErrorMessage = "O CPF/CNPJ deve conter no mínimo 11 caracteres")]
        [MaxLength(14, ErrorMessage = "O CPF/CNPJ deve conter no máximo 14 caracteres")]
        public string CPF_CNPJ { get; set; }

        [Required(ErrorMessage = "Número de celular é obrigatório")]
        [StringLength(11)]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres")]
        public string Senha { get; set; }

        [Required]
        public bool Aceite { get; set; }

        //[ForeignKey("Endereco")]
        //public int IdEndereço { get; set; }
        //public Endereco Endereco { get; set; }

        //[ForeignKey("TipoUsuario")]
        //public int IdTipoUsuario { get; set; }
        //public TipoUsuario TipoUsuario { get; set; }

        //public ICollection<Pedido> Pedidos { get; set; }

        //public ICollection<Cartao> Cartoes { get; set; }

    }
}
