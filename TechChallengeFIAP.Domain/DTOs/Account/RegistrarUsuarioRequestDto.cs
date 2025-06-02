namespace TechChallengeFIAP.Domain.DTOs.Account;

public class RegistrarUsuarioRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string SenhaConfirmada { get; set; } = string.Empty;
    public DateOnly? DataNascimento { get; set; }
    public bool EhAdministrador { get; set; }
}
