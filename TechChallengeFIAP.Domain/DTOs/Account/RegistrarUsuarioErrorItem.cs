namespace TechChallengeFIAP.Domain.DTOs.Account;

public class RegistrarUsuarioErrorItem
{
    public string Campo { get; set; } = string.Empty;
    public List<CampoErrorDetail> Errors { get; set; } = [];
}
