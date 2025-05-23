namespace TechChallengeFIAP.DTOs.Jogo
{
    public class CriarJogoRequestDto
    {

        public string NomeJogo { get; set; }
        public string DescricaoJogo { get; set; }
        public string ClassificacaoJogo { get; set; }
        public DateTime DataLancamento { get; set; }
        public decimal ValorBase { get; set; }

    }
}
