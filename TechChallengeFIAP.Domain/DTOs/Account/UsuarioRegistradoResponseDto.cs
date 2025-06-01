namespace TechChallengeFIAP.Domain.DTOs.Account;

public record UsuarioRegistradoResponseDto(Guid UsuarioId, bool EhAdministrador) : IRegistrarUsuarioResponseDto
{
    bool IRegistrarUsuarioResponseDto.IsSuccessful => true;
}
