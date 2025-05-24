using TechChallengeFIAP.Domain.Dtos;

namespace TechChallengeFIAP.Application.Interfaces;

public interface IAuthService
{
    public Task<ILoginResponse> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken);
}
