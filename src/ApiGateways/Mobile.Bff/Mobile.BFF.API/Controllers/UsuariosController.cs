using Microsoft.AspNetCore.Mvc;
using Mobile.BFF.API.Models;

namespace Mobile.BFF.API.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(ILogger<UsuariosController> logger)
        {
            _logger = logger;
        }

        [HttpPost()]
        [Route("autenticacao")]
        public async Task<IActionResult> AutenticarUsuario([FromBody] AutenticacaoUsuarioRequest requisicao)
        {
            _logger.LogInformation($"Iniciando autentica��o do usu�rio {requisicao.Cpf}");

            return Ok(new AutenticacaoUsuarioResponse(true, DateTime.Now, DateTime.Now, "ABC"));
        }
    }
}