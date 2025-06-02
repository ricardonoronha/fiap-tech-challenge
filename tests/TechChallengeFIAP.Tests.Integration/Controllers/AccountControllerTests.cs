using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TechChallengeFIAP.Tests.Integration.Fixtures;
using TechChallengeFIAP.Application.Interfaces;
using TechChallengeFIAP.Application.Services;
using TechChallengeFIAP.Domain.DTOs.Jogo;
using TechChallengeFIAP.Domain.DTOs.Account;
using System.Net.Http.Json;

namespace TechChallengeFIAP.Tests.Integration.Controllers;

[Collection("SqlServerTestCollection")]
public class AccountControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    private readonly WebAppTestFixture _webAppTest;

    public AccountControllerTests(SqlServerTestContainerFixture dbFixture)
    {
        _webAppTest = new WebAppTestFixture(dbFixture.ConnectionString);
        client = _webAppTest.CreateClient();
    }

    [Fact]
    public async Task RegistrarDeveRetornarBadRequestComDadosInvalidos()
    {
        var request = new RegistrarUsuarioRequestDto()
        {
            Email = "teste",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            NomeCompleto = string.Empty,
            Senha = "123",
            SenhaConfirmada = "1234",
            EhAdministrador = false
        };

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("api/account/register", content);
        var responseData = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task RegistrarDeveRetornarOkIdERole(bool ehAdministrador)
    {
        client.DefaultRequestHeaders.Authorization = await CreateAuthenticationHeaderValue(true);

        string nomeUsuario = $"usuario{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

        var request = new RegistrarUsuarioRequestDto()
        {
            Email = $"{nomeUsuario}@teste.com",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            NomeCompleto = nomeUsuario,
            Senha = "Forte@123",
            SenhaConfirmada = "Forte@123",
            EhAdministrador = ehAdministrador
        };

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/account/register", content);
        var responseData = await response.Content.ReadFromJsonAsync<UsuarioRegistradoResponseDto>();
        Assert.NotNull(responseData);
        Assert.NotEqual(Guid.Empty, responseData.UsuarioId);
        Assert.Equal(ehAdministrador, responseData.EhAdministrador);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task RegistrarDeveRetornarBadRequestAoUsuarioComumTentarCadastrarAdministrador()
    {
        client.DefaultRequestHeaders.Authorization = await CreateAuthenticationHeaderValue(false);

        string nomeUsuario = $"usuario{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

        var request = new RegistrarUsuarioRequestDto()
        {
            Email = $"{nomeUsuario}@teste.com",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            NomeCompleto = nomeUsuario,
            Senha = "Forte@123",
            SenhaConfirmada = "Forte@123",
            EhAdministrador = true
        };

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/account/register", content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private async Task<AuthenticationHeaderValue> CreateAuthenticationHeaderValue(bool ehAdministrador)
    {
        var authService = _webAppTest.Services.GetRequiredService<IAuthService>() as AuthService;
        var token = await authService!.GerarTokenAsync(Guid.NewGuid(), "teste@teste.com", ehAdministrador);
        return new AuthenticationHeaderValue("Bearer", token);
    }
}
