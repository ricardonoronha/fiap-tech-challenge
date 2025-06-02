using TechChallengeFIAP.Domain.DTOs.Account;
using TechChallengeFIAP.Domain.DTOs.Seguranca;


namespace TechChallengeFIAP.Domain.Interfaces;

public interface IAccountService
{
    Task<IRegistrarUsuarioResponseDto> RegistrarUsuario(RegistrarUsuarioRequestDto request, UserInfo? userInfo, CancellationToken cancellationToken);
}