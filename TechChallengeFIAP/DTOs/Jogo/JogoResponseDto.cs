using TechChallengeFIAP.DTOs.Promocao;

namespace TechChallengeFIAP.DTOs.Jogo
{
    public class JogoResponseDto
    {

        public Guid Id { get; set; }
        public string NomeJogo { get; set; }
        public string DescricaoJogo { get; set; }
        public string ClassificacaoJogo { get; set; }
        public DateTime DataLancamento { get; set; }
        public decimal ValorBase { get; set; }
        public bool EhInativo { get; set; }
        public List<PromocaoResponseDto> Promocoes { get; set; }

    }
}
