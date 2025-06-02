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

namespace TechChallengeFIAP.Tests.Integration.Controllers;

[Collection("SqlServerTestCollection")]
public class JogosControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    private readonly WebAppTestFixture _webAppTest;

    public JogosControllerTests(SqlServerTestContainerFixture dbFixture)
    {
        _webAppTest = new WebAppTestFixture(dbFixture.ConnectionString);
        client = _webAppTest.CreateClient();
    }

    [Fact]
    public async Task CriarJogoDeveRetornarUnauthorizedParaUsuarioNaoAutenticado()
    {
        var response = await client.PostAsync("/api/jogos", new StringContent("{}", Encoding.UTF8, "application/json"));
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CriarJogoDeveRetornarForbiddenParaUsuarioSemRoleAdministrador()
    {
        client.DefaultRequestHeaders.Authorization = await CreateAuthenticationHeaderValue(false);

        var response = await client.PostAsync("/api/jogos", new StringContent("{}", Encoding.UTF8, "application/json"));
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task CriarJogoDeveRetornarCreatedParaUsuarioComRoleAdministrador()
    {
        client.DefaultRequestHeaders.Authorization = await CreateAuthenticationHeaderValue(true);

        var request = new CriarJogoRequestDto()
        {
            ClassificacaoJogo = "L",
            DataLancamento = DateTime.Now.AddYears(-1),
            DescricaoJogo = "Descricao Jogo Teste",
            NomeJogo = "Jogo Teste",
            ValorBase = 100.00m
        };

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/jogos", content);

         Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    private async Task<AuthenticationHeaderValue> CreateAuthenticationHeaderValue(bool ehAdministrador)
    {
        var authService = _webAppTest.Services.GetRequiredService<IAuthService>() as AuthService;
        var token = await authService!.GerarTokenAsync(Guid.NewGuid(), "teste@teste.com", ehAdministrador);
        return new AuthenticationHeaderValue("Bearer", token);
    }
}
