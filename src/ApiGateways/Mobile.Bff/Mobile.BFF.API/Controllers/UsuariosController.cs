using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mobile.BFF.API.Application.Command.AutenticarUsuario;
using Mobile.BFF.API.Models;
using Mobile.BFF.API.Services;

namespace Mobile.BFF.API.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IMediator _mediator;

        public UsuariosController(ILogger<UsuariosController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost()]
        [Route("autenticacao")]
        public async Task<IActionResult> AutenticarUsuario([FromBody] AutenticarUsuarioCommand requisicao)
        {
            _logger.LogInformation($"[UsuariosController] Iniciando autenticação do usuário {requisicao.Cpf}");
            var resultadoAutenticacao = await _mediator.Send(requisicao);

            if (resultadoAutenticacao.Autenticado)
                return Ok(resultadoAutenticacao);

            return Unauthorized(resultadoAutenticacao);
        }
    }
}