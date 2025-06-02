using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using TechChallengeFIAP.Domain.DTOs.Seguranca; 
using TechChallengeFIAP.Domain.Interfaces;

public class UserInfoService : IUserInfoService
{
    private readonly IHttpContextAccessor _accessor;

    public UserInfoService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public UserInfo? ObterUsuario()
    {
        var user = _accessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
            return null;

        return new UserInfo
        {
            Id = Guid.TryParse(user.FindFirstValue(JwtRegisteredClaimNames.Sub), out var id) ? id : Guid.Empty,
            Email = user.FindFirstValue(JwtRegisteredClaimNames.Email) ?? string.Empty,
            Nome = user.FindFirstValue(ClaimTypes.Name) ?? string.Empty, // Só se você quiser usar futuramente
            EhAdministrador = user.IsInRole("Administrador")
        };
    }
}