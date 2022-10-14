using Microsoft.AspNetCore.Mvc;
using Mobile.BFF.API.Models;
using Mobile.BFF.API.Services;

namespace Mobile.BFF.API.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IGestaoAcessoService _gestaoAcessoService;

        public UsuariosController(ILogger<UsuariosController> logger, IGestaoAcessoService gestaoAcessoService)
        {
            _logger = logger;
            _gestaoAcessoService = gestaoAcessoService;
        }

        [HttpPost()]
        [Route("autenticacao")]
        public async Task<IActionResult> AutenticarUsuario([FromBody] AutenticacaoUsuarioRequest requisicao)
        {
            _logger.LogInformation($"Iniciando autenticação do usuário {requisicao.Cpf}");
            var resultadoAutenticacao = await _gestaoAcessoService.AutenticarUsuario(requisicao);

            return Ok(resultadoAutenticacao);
        }
    }
}