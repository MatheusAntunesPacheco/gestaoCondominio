using GestaoAcesso.API.Application.Command.AutenticarUsuario;
using GestaoAcesso.API.Application.Command.CadastrarUsuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoAcesso.Controllers
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
        
        /// <summary>
        /// Cadastrar usu�rio
        /// </summary>
        /// <param name="cadastrarUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CadastrarUsuario(CadastrarUsuarioCommand cadastrarUsuarioCommand)
        {
            _logger.LogInformation($"[UsuarioController] Cadastrando usu�rio {cadastrarUsuarioCommand.Nome}");
            var resultado = await _mediator.Send(cadastrarUsuarioCommand);

            return Ok(resultado);
        }

        [HttpPost]
        [Route("autenticacao")]
        public async Task<IActionResult> AutenticarUsuario(AutenticarUsuarioCommand autenticarUsuarioCommand)
        {
            _logger.LogInformation($"[UsuarioController] Autenticando usu�rio {autenticarUsuarioCommand.Cpf}");
            var resultado = await _mediator.Send(autenticarUsuarioCommand);

            if (resultado.Autenticado)
                return Ok(resultado);

            return Unauthorized();
        }
    }
}