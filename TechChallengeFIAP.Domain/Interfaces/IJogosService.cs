using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechChallengeFIAP.Domain.DTOs.Jogo;
using TechChallengeFIAP.Domain.DTOs.Promocao;

namespace TechChallengeFIAP.Domain.Interfaces
{
    public interface IJogosService
    {

        Task<IEnumerable<JogoResponseDto>> GetJogosAsync();
        Task<JogoResponseDto?> GetJogoAsync(Guid id);
        Task<CriarJogoResponseDto> CriarJogoAsync(CriarJogoRequestDto dto);
        Task<bool> AtualizarJogoAsync(Guid id, CriarJogoRequestDto dto);
        Task<bool> InativarJogoAsync(Guid id);
        Task<CriarPromocaoResponseDto?> CriarPromocaoAsync(Guid jogoId, CriarPromocaoRequestDto dto);
        Task<bool> CancelarPromocaoAsync(Guid promocaoId);
        Task<IEnumerable<PromocaoResponseDto>> GetPromocoesAtivasAsync();

    }

}
