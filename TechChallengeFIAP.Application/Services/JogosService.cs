using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Data.Repositorios;
using TechChallengeFIAP.Domain.DTOs.Jogo;
using TechChallengeFIAP.Domain.DTOs.Promocao;
using TechChallengeFIAP.Domain.Interfaces;
using TechChallengeFIAP.Domain.Entidades;


namespace TechChallengeFIAP.Application.Services
{
    public class JogosService : IJogosService
    {
        private readonly ApplicationDbContext _dbContext;

        public JogosService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<JogoResponseDto>> GetJogosAsync()
        {
            var jogos = await _dbContext.Jogo
                .Include(j => j.Promocoes)
                .Where(j => !j.EhInativo)
                .ToListAsync();

            var agora = DateTime.UtcNow;

            return jogos.Select(j =>
            {
                var (valorFinal, ehPromocional) = j.ObterValorFinal();

                return new JogoResponseDto
                {
                    Id = j.Id,
                    NomeJogo = j.NomeJogo,
                    DescricaoJogo = j.DescricaoJogo,
                    ClassificacaoJogo = j.ClassificacaoJogo,
                    DataLancamento = j.DataLancamento,
                    ValorBase = j.ValorBase,
                    PrecoFinal = Math.Round(valorFinal, 2),
                    EhPromocional = ehPromocional,
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
                };
            });
        }

        public async Task<JogoResponseDto?> GetJogoAsync(Guid id)
        {
            var jogo = await _dbContext.Jogo
                .Include(j => j.Promocoes)
                .FirstOrDefaultAsync(j => j.Id == id && !j.EhInativo);

            if (jogo == null)
                return null;

            var (valorFinal, ehPromocional) = jogo.ObterValorFinal();

            return new JogoResponseDto
            {
                Id = jogo.Id,
                NomeJogo = jogo.NomeJogo,
                DescricaoJogo = jogo.DescricaoJogo,
                ClassificacaoJogo = jogo.ClassificacaoJogo,
                DataLancamento = jogo.DataLancamento,
                ValorBase = jogo.ValorBase,
                PrecoFinal = Math.Round(valorFinal, 2),
                EhPromocional = ehPromocional,
                EhInativo = jogo.EhInativo,
                Promocoes = jogo.Promocoes
                    .Where(p => !p.EhCancelada)
                    .Select(p => new PromocaoResponseDto
                    {
                        Id = p.Id,
                        DataInicio = p.DataInicio,
                        DataFim = p.DataFim,
                        PercentualDesconto = p.PercentualDesconto,
                        EhCancelada = p.EhCancelada
                    }).ToList()
            };
        }

        public async Task<CriarJogoResponseDto> CriarJogoAsync(CriarJogoRequestDto dto)
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

            _dbContext.Jogo.Add(jogo);
            await _dbContext.SaveChangesAsync();

            return new CriarJogoResponseDto
            {
                Id = jogo.Id,
                NomeJogo = jogo.NomeJogo
            };
        }

        public async Task<bool> AtualizarJogoAsync(Guid id, CriarJogoRequestDto dto)
        {
            var jogo = await _dbContext.Jogo.FindAsync(id);
            if (jogo == null) return false;

            jogo.NomeJogo = dto.NomeJogo;
            jogo.DescricaoJogo = dto.DescricaoJogo;
            jogo.ClassificacaoJogo = dto.ClassificacaoJogo;
            jogo.DataLancamento = dto.DataLancamento;
            jogo.ValorBase = dto.ValorBase;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InativarJogoAsync(Guid id)
        {
            var jogo = await _dbContext.Jogo.FindAsync(id);
            if (jogo == null) return false;

            jogo.EhInativo = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CriarPromocaoResponseDto?> CriarPromocaoAsync(Guid jogoId, CriarPromocaoRequestDto dto)
        {
            var jogo = await _dbContext.Jogo.FindAsync(jogoId);
            if (jogo == null || jogo.EhInativo) return null;

            var promocao = new Promocao
            {
                Id = Guid.NewGuid(),
                JogoId = jogoId,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                PercentualDesconto = dto.PercentualDesconto,
                EhCancelada = false
            };

            _dbContext.Promocao.Add(promocao);
            await _dbContext.SaveChangesAsync();

            return new CriarPromocaoResponseDto
            {
                Id = promocao.Id,
                JogoId = jogoId,
                PercentualDesconto = promocao.PercentualDesconto
            };
        }

        public async Task<bool> CancelarPromocaoAsync(Guid promocaoId)
        {
            var promocao = await _dbContext.Promocao.FindAsync(promocaoId);
            if (promocao == null) return false;

            promocao.EhCancelada = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PromocaoResponseDto>> GetPromocoesAtivasAsync()
        {
            var agora = DateTime.UtcNow;

            var promocoes = await _dbContext.Promocao
                .Include(p => p.Jogo)
                .Where(p => !p.EhCancelada && p.DataInicio <= agora && p.DataFim >= agora && !p.Jogo.EhInativo)
                .ToListAsync();

            return promocoes.Select(p => new PromocaoResponseDto
            {
                Id = p.Id,
                DataInicio = p.DataInicio,
                DataFim = p.DataFim,
                PercentualDesconto = p.PercentualDesconto,
                EhCancelada = p.EhCancelada
            });
        }

    }

}
