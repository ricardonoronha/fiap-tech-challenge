using Microsoft.EntityFrameworkCore.Migrations.Operations;
using TechChallengeFIAP.Domain.DTOs.Account;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Application.Services;

public class SenhaValidator : ISenhaValidator
{
    public IEnumerable<CampoErrorDetail> Validar(string senha)
    {
        var resultado = new List<CampoErrorDetail>();

        if (senha.Length < 8)
        {
            resultado.Add(new()
            {
                Codigo = "TamanhoInvalido",
                Descricao = "Senha deve ter pelo menos oito caracteres"
            });
        }

        if (!senha.Any(char.IsLetter))
        {
            resultado.Add(new()
            {
                Codigo = "SemLetras",
                Descricao = "Senha deve conter pelo menos uma letra maiúscula ou menúscula"
            });
        }

        if (!senha.Any(char.IsDigit))
        {
            resultado.Add(new()
            {
                Codigo = "SemNumeros",
                Descricao = "Senha deve conter pelo menos um dígito"
            });
        }

        if (!senha.Any(c => !char.IsLetterOrDigit(c)))
        {
            resultado.Add(new()
            {
                Codigo = "SemEspeciais",
                Descricao = "Senha deve conter pelo menos um caractere especial"
            });
        }

        return resultado;
    }
}