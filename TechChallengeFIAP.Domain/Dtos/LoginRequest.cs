using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFIAP.Domain.Dtos
{
    public class LoginRequest
    {
        [Required(ErrorMessage ="E-mail é obrigatório para realizar login")]
        [EmailAddress(ErrorMessage = "O valor fornecido não é um e-mail válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage ="Senha é obrigatória para realizar login")]
        public string Senha { get; set; } = string.Empty;
    }
}
