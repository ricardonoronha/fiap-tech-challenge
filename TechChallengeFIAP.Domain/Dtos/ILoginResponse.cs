using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFIAP.Domain.Dtos
{
    public interface ILoginResponse
    {
        bool IsSuccessful { get; }
    }

    public record LoginBemSucedidoResponse(string Nome, string Email, string Token, bool EhAdministrador) : ILoginResponse
    {
        bool ILoginResponse.IsSuccessful => true;
    }

    public record LoginFalhoResponse(string MensagemErro) : ILoginResponse
    {
        bool ILoginResponse.IsSuccessful => false;
    }
}
