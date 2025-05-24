using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFIAP.Domain.Interfaces
{
    public interface ISenhaHasher
    {
        string HashSenha(string senha);
        bool VerificarSenha(string senhaDigitada, string hashArmazenado);
    }
}
