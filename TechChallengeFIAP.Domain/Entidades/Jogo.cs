namespace TechChallengeFIAP.Domain.Entidades;

public class Jogo
{
    public Guid Id { get; set; }
    public string NomeJogo { get; set; }
    public string DescricaoJogo { get; set; }
    public string ClassificacaoJogo { get; set; }
    public DateTime DataLancamento { get; set; }
    public decimal ValorBase { get; set; }
    public decimal ValorPromocao { get; set; }
    public bool EhInativo { get; set; }
}
