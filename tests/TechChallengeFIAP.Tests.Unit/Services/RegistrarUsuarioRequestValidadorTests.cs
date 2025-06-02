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
    private readonly RegistrarUsuarioRequestValidador _validador;

    public RegistrarUsuarioRequestValidadorTests()
    {
        _validador = new RegistrarUsuarioRequestValidador(
            _pessoaRepositorioMock.Object,
            _senhaValidatorMock.Object
        );
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

        var resultado = await _validador.ValidarAsync(request, null);

        Assert.Contains(resultado, r => r.Campo == "Email");
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

        var resultado = await _validador.ValidarAsync(request, null);

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

        var resultado = await _validador.ValidarAsync(request, null);

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

        var resultado = await _validador.ValidarAsync(request, null);

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

        var resultado = await _validador.ValidarAsync(request, null);

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

        var resultado = await _validador.ValidarAsync(request, userInfo);

        Assert.Contains(resultado, r => r.Campo == "EhAdministrador");
    }
}