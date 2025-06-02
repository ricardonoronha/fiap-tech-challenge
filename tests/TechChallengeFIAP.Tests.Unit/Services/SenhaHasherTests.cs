using Microsoft.Extensions.DependencyInjection;
using TechChallengeFIAP.Application.Services;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Tests.Unit.Services;

public class SenhaHasherTests
{
    private readonly IServiceProvider _serviceProvider;

    public SenhaHasherTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddScoped<ISenhaHasher, SenhaHasher>()
            .BuildServiceProvider();
    }

    [Fact]
    public void HashGeradoDeveValidarMesmaSenhaComSucesso()
    {
       GerarEValidarHash("teste@123", "teste@123", true);
    }

    [Fact]
    public void HashGeradoDeveValidarSenhaDiferenteComFalha()
    {
        GerarEValidarHash("teste@123", "teste@456", false);
    }

    private void GerarEValidarHash(string senha, string novaSenha, bool valorEsperado)
    {
        var hasher = _serviceProvider.GetRequiredService<ISenhaHasher>();
        string senhaTeste = senha;
        var hashSenha = hasher.HashSenha(senhaTeste);
        Assert.Equal(valorEsperado, hasher.VerificarSenha(novaSenha, hashSenha));
    }
}