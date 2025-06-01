using TechChallengeFIAP.Domain.DTOs.Account;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Application.Services;

public class SenhaValidator : ISenhaValidator
{
    public IEnumerable<CampoErrorDetail> Validar(string senha)
    {
        return [];
    }
}