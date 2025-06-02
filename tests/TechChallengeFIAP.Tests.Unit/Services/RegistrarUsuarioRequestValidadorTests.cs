using Microsoft.Extensions.DependencyInjection;
using Moq;
using TechChallengeFIAP.Application.Services;
using TechChallengeFIAP.Domain.DTOs.Account;
using TechChallengeFIAP.Domain.DTOs.Seguranca;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Tests.Unit.Services;

public class RegistrarUsuarioRequestValidadorTests
{
    private readonly Mock<IPessoaRepositorio> _pessoaRepositorioMock = new();
    private readonly Mock<ISenhaValidator> _senhaValidatorMock = new();
    private readonly IServiceProvider _serviceProvider;

    public RegistrarUsuarioRequestValidadorTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddSingleton(_pessoaRepositorioMock.Object)
            .AddSingleton(_senhaValidatorMock.Object)
            .AddScoped<IRegistrarUsuarioRequestValidador, RegistrarUsuarioRequestValidador>()
            .BuildServiceProvider();
    }

    [Fact]
    public async Task DeveRetornarErroQuandoEmailEstiverEmBranco()
    {
        var request = new RegistrarUsuarioRequestDto
        {
            Email = "",
            NomeCompleto = "Nome",
            Senha = "Senha@123",
            SenhaConfirmada = "Senha@123",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            EhAdministrador = false
        };

        var validador = _serviceProvider.GetRequiredService<IRegistrarUsuarioRequestValidador>();

        var resultado = await validador.ValidarAsync(request, null);

        Assert.Contains(resultado, r => r.Campo == "Email");
    }

    [Fact]
    public async Task DeveRetornarErroQuandoSenhasEstiverEmBranco()
    {
        var request = new RegistrarUsuarioRequestDto
        {
            Email = "teste@teste.com",
            NomeCompleto = "Nome",
            Senha = "",
            SenhaConfirmada = "",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            EhAdministrador = false
        };

        var validador = _serviceProvider.GetRequiredService<IRegistrarUsuarioRequestValidador>();

        var resultado = await validador.ValidarAsync(request, null);

        Assert.Contains(resultado, r => r.Campo == "Senha");
    }

    [Fact]
    public async Task DeveRetornarErroQuandoSenhasNaoAtendemRequisitosDeSeguranca()
    {
        _senhaValidatorMock.Setup(x => x.Validar(It.IsAny<string>()))
        .Returns(
        [
            new(){ Codigo = "TestCodigo", Descricao =  "Teste descrição" }
        ]);

        var request = new RegistrarUsuarioRequestDto
        {
            Email = "teste@teste.com",
            NomeCompleto = "Nome",
            Senha = "F@12",
            SenhaConfirmada = "F@12",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            EhAdministrador = false
        };

        var validador = _serviceProvider.GetRequiredService<IRegistrarUsuarioRequestValidador>();

        var resultado = await validador.ValidarAsync(request, null);

        Assert.Contains(resultado, r => r.Campo == "Senha");
    }

    [Fact]
    public async Task DeveRetornarErroQuandoEmailEstiverEmFormatoInvalido()
    {
        var request = new RegistrarUsuarioRequestDto
        {
            Email = "invalid-email",
            NomeCompleto = "Nome",
            Senha = "Senha@123",
            SenhaConfirmada = "Senha@123",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            EhAdministrador = false
        };

        _pessoaRepositorioMock.Setup(p => p.VerificarEhEmailIndisponivel(It.IsAny<string>()))
                              .ReturnsAsync(false);

        var validador = _serviceProvider.GetRequiredService<IRegistrarUsuarioRequestValidador>();

        var resultado = await validador.ValidarAsync(request, null);

        Assert.Contains(resultado, r => r.Campo == "Email" &&
                                        r.Errors.Exists(e => e.Codigo == "FormatoInvalido"));
    }

    [Fact]
    public async Task DeveRetornarErroSeEmailJaEstiverEmUso()
    {
        _pessoaRepositorioMock.Setup(p => p.VerificarEhEmailIndisponivel("teste@teste.com"))
                              .ReturnsAsync(true);

        var request = new RegistrarUsuarioRequestDto
        {
            Email = "teste@teste.com",
            NomeCompleto = "Nome",
            Senha = "Senha@123",
            SenhaConfirmada = "Senha@123",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            EhAdministrador = false
        };

        var validador = _serviceProvider.GetRequiredService<IRegistrarUsuarioRequestValidador>();

        var resultado = await validador.ValidarAsync(request, null);

        Assert.Contains(resultado, r => r.Campo == "Email" &&
                                        r.Errors.Exists(e => e.Codigo == "Indisponivel"));
    }

    [Fact]
    public async Task DeveRetornarErroQuandoSenhasSaoDiferentes()
    {
        var request = new RegistrarUsuarioRequestDto
        {
            Email = "teste@teste.com",
            NomeCompleto = "Nome",
            Senha = "Senha@123",
            SenhaConfirmada = "OutraSenha@123",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            EhAdministrador = false
        };

        var validador = _serviceProvider.GetRequiredService<IRegistrarUsuarioRequestValidador>();

        var resultado = await validador.ValidarAsync(request, null);

        Assert.Contains(resultado, r => r.Campo == "Senha" &&
                                        r.Errors.Exists(e => e.Codigo == "ConfirmacaoDiferente"));
    }

    [Fact]
    public async Task DeveRetornarErroSeDataNascimentoNaoForPreenchida()
    {
        var request = new RegistrarUsuarioRequestDto
        {
            Email = "teste@teste.com",
            NomeCompleto = "Nome",
            Senha = "Senha@123",
            SenhaConfirmada = "Senha@123",
            DataNascimento = null,
            EhAdministrador = false
        };

        var validador = _serviceProvider.GetRequiredService<IRegistrarUsuarioRequestValidador>();

        var resultado = await validador.ValidarAsync(request, null);

        Assert.Contains(resultado, r => r.Campo == "DataNascimento");
    }

    [Fact]
    public async Task DeveRetornarErroSeTentarCadastrarAdministradorSemPermissao()
    {
        var request = new RegistrarUsuarioRequestDto
        {
            Email = "teste@teste.com",
            NomeCompleto = "Nome",
            Senha = "Senha@123",
            SenhaConfirmada = "Senha@123",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            EhAdministrador = true
        };

        var userInfo = new UserInfo { EhAdministrador = false };

        var validador = _serviceProvider.GetRequiredService<IRegistrarUsuarioRequestValidador>();

        var resultado = await validador.ValidarAsync(request, userInfo);

        Assert.Contains(resultado, r => r.Campo == "EhAdministrador");
    }
}