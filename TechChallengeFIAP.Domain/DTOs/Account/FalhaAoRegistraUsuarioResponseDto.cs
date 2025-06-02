namespace TechChallengeFIAP.Domain.DTOs.Account;

public record FalhaAoRegistraUsuarioResponseDto(IEnumerable<RegistrarUsuarioErrorItem> Erros) : IRegistrarUsuarioResponseDto
{
    bool IRegistrarUsuarioResponseDto.IsSuccessful => false;
}
