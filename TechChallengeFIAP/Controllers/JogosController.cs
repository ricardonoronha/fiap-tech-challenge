using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Data.Repositorios;
using TechChallengeFIAP.Domain.DTOs.Jogo;
using TechChallengeFIAP.Domain.DTOs.Promocao;

namespace TechChallengeFIAP.Controllers
{

    [ApiController]
    [Route("api/jogos")]
    public class JogosController : ControllerBase
    {
        private readonly IJogosService _jogosService;

        public JogosController(IJogosService jogosService)
        {
            _jogosService = jogosService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoResponseDto>>> GetJogos()
        {
            try
            {
                var jogos = await _jogosService.GetJogosAsync();
                return Ok(jogos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JogoResponseDto>> GetJogo(Guid id)
        {
            try
            {
                var jogo = await _jogosService.GetJogoAsync(id);
                if (jogo == null)
                    return NotFound();

                return Ok(jogo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CriarJogoResponseDto>> CriarJogo([FromBody] CriarJogoRequestDto dto)
        {
            try
            {
                var jogoCriado = await _jogosService.CriarJogoAsync(dto);
                return CreatedAtAction(nameof(GetJogo), new { id = jogoCriado.Id }, jogoCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AtualizarJogo(Guid id, [FromBody] CriarJogoRequestDto dto)
        {
            try
            {
                var atualizado = await _jogosService.AtualizarJogoAsync(id, dto);
                if (!atualizado)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> InativarJogo(Guid id)
        {
            try
            {
                var inativado = await _jogosService.InativarJogoAsync(id);
                if (!inativado)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{jogoId}/promocoes")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CriarPromocao(Guid jogoId, [FromBody] CriarPromocaoRequestDto dto)
        {
            try
            {
                var promocao = await _jogosService.CriarPromocaoAsync(jogoId, dto);
                if (promocao == null)
                    return NotFound();

                return Ok(promocao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("promocoes/{promocaoId}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CancelarPromocao(Guid promocaoId)
        {
            try
            {
                var cancelado = await _jogosService.CancelarPromocaoAsync(promocaoId);
                if (!cancelado)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
