namespace TechChallengeFIAP.Domain.Entidades;

public class Jogo
{
    string NomeJogo { get; set; }
    string DescricaoJogo { get; set; }
    string ClassificacaoJogo { get; set; }
    decimal ValorJogo { get; set; }
    decimal ValorPromocao { get; set; }
    DateTime DataLancamento { get; set; }
    bool PromocaoHabilitada { get; set; }
}
