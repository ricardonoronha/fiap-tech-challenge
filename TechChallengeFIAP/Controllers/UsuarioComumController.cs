using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFIAP.Controllers
{

    [ApiController]

    [Route("fiapcloudgames/user")]

    public class UsuarioComumController
    {

        [HttpGet]
        public async Task<ActionResult> ListarJogos()
        {
            return null;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ListarUmJogo(int id)
        {
            return null;
        }

        [HttpGet("{id}")]
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

}
