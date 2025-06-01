using TechChallengeFIAP.Domain.DTOs.Account;

namespace TechChallengeFIAP.Domain.Interfaces;

public interface ISenhaValidator
{
    IEnumerable<CampoErrorDetail> Validar(string senha);
}