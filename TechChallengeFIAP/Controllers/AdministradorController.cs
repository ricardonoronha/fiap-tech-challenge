using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFIAP.Controllers
{
    [ApiController]
    [Route("fiapcloudgames/admin")]
    public class AdministradorController : ControllerBase
    {
        private readonly ILogger<AdministradorController> _logger;

        public AdministradorController(ILogger<AdministradorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> ListarUsuarios()
        {
            _logger.LogInformation("Executando ListarUsuarios");
            return Ok("Usuários listados.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ListarUmUsuario(int id)
        {
            _logger.LogInformation("Buscando usuário com ID: {Id}", id);
            return Ok($"Usuário {id}");
        }

        [HttpPatch("{id}/bloquear")]
        public async Task<ActionResult> BloquearUsuario(int id)
        {
            _logger.LogWarning("Usuário {Id} será bloqueado", id);
            return Ok($"Usuário {id} bloqueado.");
        }

        [HttpPatch("{id}/permissao")]
        public async Task<ActionResult> AlterarPermissaoUsuario(int id)
        {
            _logger.LogInformation("Alterando permissão do usuário {Id}", id);
            return Ok($"Permissão do usuário {id} alterada.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterarDadosUsuario(int id)
        {
            _logger.LogInformation("Alterando dados do usuário {Id}", id);
            return Ok($"Dados do usuário {id} atualizados.");
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarJogo()
        {
            _logger.LogInformation("Cadastrando novo jogo.");
            return Ok("Jogo cadastrado.");
        }

        [HttpPatch("{id}/promocao")]
        public async Task<ActionResult> CriarPromocaoJogo(int id)
        {
            _logger.LogInformation("Criando promoção para o jogo {Id}", id);
            return Ok($"Promoção criada para o jogo {id}.");
        }
    }
}
