using System.ComponentModel.DataAnnotations;

public class RegisterAccountRequestDto

{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string NomeCompleto { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;

    [Required]
    public string SenhaConfirmada { get; set; } = string.Empty;

    [Required]
    public DateTime DataNascimento { get; set; }
}

public class RegisterAccountResponseDto
{
    
}