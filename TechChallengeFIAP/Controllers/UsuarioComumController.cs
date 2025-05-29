using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Data.Repositorios;

namespace TechChallengeFIAP.Controllers;


[ApiController]

[Route("fiapcloudgames/user")]

public class UsuarioComumController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    public UsuarioComumController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult> ListarJogos()
    {
        var jogos = dbContext.Jogo.ToList();
        return Ok(jogos);
    }

    [HttpGet("ListarUmJogo/{id}")]
    public async Task<ActionResult> ListarUmJogo(int id)
    {
        return null;
    }

    [HttpGet("VisualizarSaldo/{id}")]
    public async Task<ActionResult> VisualizarSaldo(int id)
    {
        return null;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> AlterarDadosPessoais(int id)
    {
        return null;
    }

    [HttpPost]
    public async Task<ActionResult> CadastrarLogin()
    {
        return null;
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> ObterJogo(int id)
    {
        return null;
    }

}
