namespace TechChallengeFIAP.Domain.Entidades;

public class Jogo
{
    public Guid Id { get; set; }
    public string NomeJogo { get; set; } = string.Empty;
    public string DescricaoJogo { get; set; } = string.Empty;
    public string ClassificacaoJogo { get; set; } = "L";
    public DateTime DataLancamento { get; set; }
    public decimal ValorBase { get; set; }
    public decimal ValorPromocao { get; set; }
    public bool EhInativo { get; set; }

    public ICollection<Promocao> Promocoes { get; set; } = [];

    public (decimal valorFinal, bool ehPromocional) ObterValorFinal()
    {
        DateTime dataAtual = DateTime.Today;

        var promocaoAtiva = Promocoes
               .FirstOrDefault(p => !p.EhCancelada && p.DataInicio.Date <= dataAtual && p.DataFim.Date >= dataAtual);

        var ehPromocional = promocaoAtiva != null;

        var valorFinal = ehPromocional
            ? ValorBase * (1 - (promocaoAtiva!.PercentualDesconto / 100))
            : ValorBase;

        return (valorFinal, ehPromocional);
    }
}
