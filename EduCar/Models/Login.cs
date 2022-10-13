using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduCar.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email é obrigatório"), EmailAddress(ErrorMessage = "O formato do e-mail é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }
    }
}
