namespace TechChallengeFIAP.Domain.DTOs.Seguranca;

public class UserInfo
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Email { get; init; } = string.Empty;
    public string Nome { get; init; } = string.Empty;
    public bool EhAdministrador { get; init; }
}