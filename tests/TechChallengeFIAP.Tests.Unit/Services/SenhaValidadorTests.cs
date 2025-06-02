using Microsoft.Extensions.DependencyInjection;
using TechChallengeFIAP.Application.Services;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Tests.Unit.Services;

public class SenhaValidadorTests
{
    private readonly IServiceProvider _serviceProvider;

    public SenhaValidadorTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddScoped<ISenhaValidator, SenhaValidator>()
            .BuildServiceProvider();
    }

    [Theory]
    [InlineData("Forte@123", true, "")]
    [InlineData("A1@z", false, "TamanhoInvalido")]
    [InlineData("Segura123", false, "SemEspeciais")]
    [InlineData("Senha@forte", false, "SemNumeros")]  
    [InlineData("12345678@", false, "SemLetras")]  
    public void SenhaDeveSerValidadaConformeEsperado(string senha, bool ehValido, string codigoErroEsperado)
    {
        var validadorSenha = _serviceProvider.GetRequiredService<ISenhaValidator>();
        var resultadoValidacao = validadorSenha.Validar(senha);

        Assert.Equal(ehValido, !resultadoValidacao.Any());

        string codigoErroObtido = resultadoValidacao.FirstOrDefault()?.Codigo ?? "";
        Assert.Equal(codigoErroEsperado, codigoErroObtido);
    }
}