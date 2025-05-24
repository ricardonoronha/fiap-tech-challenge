using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFIAP.Domain.Dtos
{
    public class LoginResponse
    {
        public bool IsSuccessful { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool EhAdministrador { get; set; }
        public string MensagemErro { get; set; } = string.Empty;

        public static LoginResponse Error(string mensagemErro)
            => new()
            {
                IsSuccessful = false,
                MensagemErro = mensagemErro
            };

        public static LoginResponse Sucesso(string nome, string email, bool ehAdministrador, string token)
          => new()
          {
              IsSuccessful =true,
              Nome = nome, 
              Email = email, 
              EhAdministrador = ehAdministrador,
              Token = token
          };

    }
}
