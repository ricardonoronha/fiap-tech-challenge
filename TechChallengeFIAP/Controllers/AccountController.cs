using TechChallengeFIAP.Domain.DTOs.Account;
using Microsoft.AspNetCore.Mvc;
using TechChallengeFIAP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace TechChallengeFIAP.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(IAccountService AccountService, IUserInfoService UserInfoService) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(UsuarioRegistradoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FalhaAoRegistraUsuarioResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegistrarUsuarioRequestDto request, CancellationToken cancellationToken)
        {
            var userInfo = UserInfoService.ObterUsuario();
            var resultado = await AccountService.RegistrarUsuario(request, userInfo, cancellationToken);

            if (!resultado.IsSuccessful)
                return BadRequest(resultado);

            return Ok(resultado);
        }
    }
}
