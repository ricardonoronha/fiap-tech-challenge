using Microsoft.Extensions.DependencyInjection;
using TechChallengeFIAP.Application.Services;
using TechChallengeFIAP.Domain.Entidades;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Tests.Unit.Services;

public class JogoTests
{

    [Fact]
    public void JogoComPromocaoAtivaDeveRetornarValorPromocional()
    {
        var jogo = new Jogo()
        {
            ClassificacaoJogo = "L",
            DataLancamento = DateTime.Today.AddYears(-1),
            DescricaoJogo = "Descrição Jogo Teste",
            EhInativo = true,
            NomeJogo = "Jogo Teste",
            ValorBase = 100.00m,
            Promocoes = [
                new() {
                    DataInicio = DateTime.Today.AddDays(-15),
                    DataFim = DateTime.Today.AddDays(15),
                    EhCancelada = false,
                    PercentualDesconto = 15.00m
                }
            ]
        };

        var (valorFinal, ehPromocional) = jogo.ObterValorFinal();

        Assert.Equal(85.00m, valorFinal);
        Assert.True(ehPromocional);
    }

    [Fact]
    public void JogoSemPromocaoDeveRetornarValorBase()
    {
        var jogo = new Jogo()
        {
            ClassificacaoJogo = "L",
            DataLancamento = DateTime.Today.AddYears(-1),
            DescricaoJogo = "Descrição Jogo Teste",
            EhInativo = true,
            NomeJogo = "Jogo Teste",
            ValorBase = 100.00m,
            Promocoes = []
        };

        var (valorFinal, ehPromocional) = jogo.ObterValorFinal();

        Assert.Equal(100.00m, valorFinal);
        Assert.False(ehPromocional);
    }

    [Fact]
    public void JogoComPromocaoInativaDeveRetornarValorBase()
    {
        var jogo = new Jogo()
        {
            ClassificacaoJogo = "L",
            DataLancamento = DateTime.Today.AddYears(-1),
            DescricaoJogo = "Descrição Jogo Teste",
            EhInativo = true,
            NomeJogo = "Jogo Teste",
            ValorBase = 100.00m,
            Promocoes = [
                new() {
                    DataInicio = DateTime.Today.AddDays(-15),
                    DataFim = DateTime.Today.AddDays(-5),
                    EhCancelada = false,
                    PercentualDesconto = 15.00m
                }
            ]
        };

        var (valorFinal, ehPromocional) = jogo.ObterValorFinal();

        Assert.Equal(100.00m, valorFinal);
        Assert.False(ehPromocional);
    }

}