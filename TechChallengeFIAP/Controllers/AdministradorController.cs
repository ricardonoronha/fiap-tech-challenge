using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFIAP.Controllers;


[ApiController]

[Route("fiapcloudgames/admin")]

public class AdministradorController
{

    [HttpGet]
    public async Task<ActionResult> ListarUsuarios()
    {
        return null;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> ListarUmUsuario(int id)
    {
        return null;
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> BloquearUsuario(int id)
    {
        return null;
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> AlterarPermissaoUsuario(int id)
    {
        return null;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> AlterarDadosUsuario(int id)
    {
        return null;
    }

    [HttpPost]
    public async Task<ActionResult> CadastrarJogo()
    {
        return null;
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> CriarPromocaoJogo(int id)
    {
        return null;
    }

}
