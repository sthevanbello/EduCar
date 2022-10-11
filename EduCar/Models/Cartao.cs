using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EduCar.Models
{
    public class Cartao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Número do cartão é obrigatório")]
        [StringLength(16)]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Nome do titular é obrigatório")]
        public string Titular { get; set; }

        [Required(ErrorMessage = "Nome da bandeira é obrigatório")]
        public string Bandeira { get; set; }

        [Required(ErrorMessage = "CPF/CNPJ é obrigatório")]
        [MinLength(11, ErrorMessage = "O CPF/CNPJ deve conter no mínimo 11 caracteres")]
        [MaxLength(14, ErrorMessage = "O CPF/CNPJ deve conter no máximo 14 caracteres")]
        public string CPF_CNPJ { get; set; }

        [Required(ErrorMessage = "Data de vencimento é obrigatório")]
        [RegularExpression(@"(0[1-9]|10|11|12)/20[0-9]{2}$", ErrorMessage = "Informe um vencimento válido 'xx/xxxx'")]
        public string Vencimento { get; set; }

        [Required(ErrorMessage = "CVV é obrigatório")]
        [StringLength(3, ErrorMessage = "O CVV deverá conter apenas 3 digitos")]
        public string CVV { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Usuario Usuario { get; set; }
    }
}
