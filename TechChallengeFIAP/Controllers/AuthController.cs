using TechChallengeFIAP.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using TechChallengeFIAP.Application.Interfaces;

namespace TechChallengeFIAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService AuthService) : ControllerBase
    {

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginBemSucedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(LoginFalhoResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {



            var result = await AuthService.LoginAsync(request, cancellationToken);

            if (!result.IsSuccessful)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
