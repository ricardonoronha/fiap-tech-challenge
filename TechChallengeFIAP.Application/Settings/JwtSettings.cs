using System.ComponentModel.DataAnnotations;

namespace TechChallengeFIAP.Application.Settings;

public class JwtSettings
{
    [Required] public string Secret { get; set; } = string.Empty;
    [Required] public string Issuer { get; set; } = string.Empty;
    [Required] public string Audience { get; set; } = string.Empty;
    public int TokenExpirationInMinutes { get; set; } = 60;
    public int TokenTimeToleranceInMinutes { get; set; } = 1;
}
