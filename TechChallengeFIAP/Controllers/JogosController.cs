using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Data.Repositorios;
using TechChallengeFIAP.DTOs.Jogo;
using TechChallengeFIAP.DTOs.Promocao;

namespace TechChallengeFIAP.Controllers
{
    [ApiController]
    [Route("api/jogos")]
    public class JogosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public JogosController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoResponseDto>>> GetJogos()
        {

            try
            {
                var jogos = await dbContext.Jogo
                .Include(j => j.Promocoes)
                .Where(j => !j.EhInativo)
                .ToListAsync();

                var response = jogos.Select(j => new JogoResponseDto
                {
                    Id = j.Id,
                    NomeJogo = j.NomeJogo,
                    DescricaoJogo = j.DescricaoJogo,
                    ClassificacaoJogo = j.ClassificacaoJogo,
                    DataLancamento = j.DataLancamento,
                    ValorBase = j.ValorBase,
                    EhInativo = j.EhInativo,
                    Promocoes = j.Promocoes
                        .Where(p => !p.EhCancelada)
                        .Select(p => new PromocaoResponseDto
                        {
                            Id = p.Id,
                            DataInicio = p.DataInicio,
                            DataFim = p.DataFim,
                            PercentualDesconto = p.PercentualDesconto,
                            EhCancelada = p.EhCancelada
                        }).ToList()
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JogoResponseDto>> GetJogo(Guid id)
        {

            try
            {
                var jogo = await dbContext.Jogo
               .Include(j => j.Promocoes)
               .FirstOrDefaultAsync(j => j.Id == id && !j.EhInativo);

                if (jogo == null)
                    return NotFound();

                var response = new JogoResponseDto
                {
                    Id = jogo.Id,
                    NomeJogo = jogo.NomeJogo,
                    DescricaoJogo = jogo.DescricaoJogo,
                    ClassificacaoJogo = jogo.ClassificacaoJogo,
                    DataLancamento = jogo.DataLancamento,
                    ValorBase = jogo.ValorBase,
                    EhInativo = jogo.EhInativo,
                    Promocoes = jogo.Promocoes.Select(p => new PromocaoResponseDto
                    {
                        Id = p.Id,
                        DataInicio = p.DataInicio,
                        DataFim = p.DataFim,
                        PercentualDesconto = p.PercentualDesconto,
                        EhCancelada = p.EhCancelada
                    }).ToList()
                };

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CriarJogoResponseDto>> CriarJogo([FromBody] CriarJogoRequestDto dto)
        {

            try
            {
                var jogo = new Jogo
                {
                    Id = Guid.NewGuid(),
                    NomeJogo = dto.NomeJogo,
                    DescricaoJogo = dto.DescricaoJogo,
                    ClassificacaoJogo = dto.ClassificacaoJogo,
                    DataLancamento = dto.DataLancamento,
                    ValorBase = dto.ValorBase,
                    EhInativo = false
                };

                dbContext.Jogo.Add(jogo);
                await dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetJogo), new { id = jogo.Id }, new CriarJogoResponseDto
                {
                    Id = jogo.Id,
                    NomeJogo = jogo.NomeJogo
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AtualizarJogo(Guid id, [FromBody] CriarJogoRequestDto dto)
        {

            try
            {
                var jogo = await dbContext.Jogo.FindAsync(id);
                if (jogo == null)
                    return NotFound();

                jogo.NomeJogo = dto.NomeJogo;
                jogo.DescricaoJogo = dto.DescricaoJogo;
                jogo.ClassificacaoJogo = dto.ClassificacaoJogo;
                jogo.DataLancamento = dto.DataLancamento;
                jogo.ValorBase = dto.ValorBase;

                await dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
           
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> InativarJogo(Guid id)
        {
            try
            {
                var jogo = await dbContext.Jogo.FindAsync(id);
                if (jogo == null)
                    return NotFound();

                jogo.EhInativo = true;
                await dbContext.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("{jogoId}/promocoes")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CriarPromocao(Guid jogoId, [FromBody] CriarPromocaoRequestDto dto)
        {

            try
            {
                var jogo = await dbContext.Jogo.FindAsync(jogoId);
                if (jogo == null || jogo.EhInativo)
                    return NotFound();

                var promocao = new Promocao
                {
                    Id = Guid.NewGuid(),
                    JogoId = jogoId,
                    DataInicio = dto.DataInicio,
                    DataFim = dto.DataFim,
                    PercentualDesconto = dto.PercentualDesconto,
                    EhCancelada = false
                };

                dbContext.Promocao.Add(promocao);
                await dbContext.SaveChangesAsync();

                return Ok(new CriarPromocaoResponseDto
                {
                    Id = promocao.Id,
                    JogoId = jogoId,
                    PercentualDesconto = promocao.PercentualDesconto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpDelete("promocoes/{promocaoId}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CancelarPromocao(Guid promocaoId)
        {
            try
            {
                var promocao = await dbContext.Promocao.FindAsync(promocaoId);
                if (promocao == null)
                    return NotFound();

                promocao.EhCancelada = true;
                await dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
