using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Routing.Constraints;
using Org.BouncyCastle.Asn1.Ocsp;
using TechChallengeFIAP.Domain.Dtos;
using TechChallengeFIAP.Domain.Entidades;
using TechChallengeFIAP.Domain.Interfaces;
using TechChallengeFIAP.Tests.Integration.Fixtures;

namespace TechChallengeFIAP.Tests.Integration.Controllers;

[Collection("SqlServerTestCollection")]
public class AuthControllerTests
{
    private readonly HttpClient _client;
    private readonly WebAppTestFixture _webAppTest;

    public AuthControllerTests(SqlServerTestContainerFixture dbFixture)
    {
        _webAppTest = new WebAppTestFixture(dbFixture.ConnectionString);
        _client = _webAppTest.CreateClient();
    }

    [Fact]
    public async Task DeveLogarComSucesso()
    {
        // Arrange 
        // Criando usuário para login e salvando no banco de dados
        var senhaHasher = _webAppTest.GetRequiredService<ISenhaHasher>();

        string nomeUsuario = $"usuario{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";
        string emailUsuario = $"{nomeUsuario}@teste.com";
        string senhaUsuario = "teste";

        var usuarioTest = new Pessoa()
        {
            NomeCompleto = "Usuario Test",
            EmailUsuario = emailUsuario,
            HashSenha = senhaHasher.HashSenha(senhaUsuario),
            EhAdministrador = false,
            DataNascimento = DateTime.Now.AddYears(-18),
            EhAtivo = true,
            NomeUsuario = nomeUsuario,
            DataCriacao = DateTime.Now,
            Id = Guid.NewGuid()
        };

        await _webAppTest.SaveEntities(usuarioTest);

        var loginRequest = new { Email = emailUsuario, Senha = senhaUsuario };

        // Act
        // Chamando API para login
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

        // Assert
        // Devemos ter um resposta de sucesso
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadFromJsonAsync<LoginBemSucedidoResponse>();
        Assert.NotEmpty(responseData!.Token);
    }

    [Fact]
    public async Task DeveReceberFalhaAoLogar()
    {

        var loginRequest = new { Email = "email_qualquer@teste.com", Senha = "senhaQualquerParaFalhar" };

        // Act
        // Chamando API para login
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

        // Assert
        // Devemos ter um resposta de sucesso
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}