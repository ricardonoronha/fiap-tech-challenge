using TechChallengeFIAP.Domain.Dtos;

namespace TechChallengeFIAP.Application.Interfaces;

public interface IAuthService
{
    public Task<LoginResponse> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken);
}
