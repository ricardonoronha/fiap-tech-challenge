using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFIAP.Application.Interfaces;
using TechChallengeFIAP.Application.Settings;
using TechChallengeFIAP.Domain.Dtos;
using TechChallengeFIAP.Domain.Events;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Application.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IPessoaRepositorio _pessoaRepository;
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISenhaHasher _senhaHasher;

    public AuthService(
        IOptions<JwtSettings> jwtSettings,
        IPessoaRepositorio pessoaRepository,
        IEventStoreRepository eventStoreRepository,
        IUnitOfWork unitOfWork,
        ISenhaHasher senhaHasher)
    {
        _jwtSettings = jwtSettings.Value;
        _pessoaRepository = pessoaRepository;
        _eventStoreRepository = eventStoreRepository;
        _unitOfWork = unitOfWork;
        _senhaHasher = senhaHasher;
    }


    public async Task<ILoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var events = new List<IEvent>
        {
            new TentativaAcesso(request.Email)
        };

        var pessoa = await _pessoaRepository.GetByEmail(request.Email);

        if (pessoa is not null && _senhaHasher.VerificarSenha(request.Senha, pessoa.HashSenha))
        {
            events.Add(new LoginBemSucedido(request.Email));

            var token = await GerarTokenAsync(pessoa.Id, pessoa.EmailUsuario, pessoa.EhAdministrador);

            events.Add(new TokenGerado(request.Email));

            _eventStoreRepository.SaveEvents(pessoa, events);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new LoginBemSucedidoResponse(pessoa.NomeCompleto, pessoa.EmailUsuario, token, pessoa.EhAdministrador);
        }

        events.Add(new LoginFalho(request.Email));

        _eventStoreRepository.SaveEvents(pessoa, events);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginFalhoResponse("Login inválido");
    }

    public Task<string> GerarTokenAsync(Guid userId, string email, bool ehAdministrador)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer),
            new(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience)
        ];

        if (ehAdministrador)
            claims.Add(new(ClaimTypes.Role, "Administrador"));

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return Task.FromResult(tokenHandler.WriteToken(token));
    }
}
