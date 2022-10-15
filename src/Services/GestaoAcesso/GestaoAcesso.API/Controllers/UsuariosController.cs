using GestaoAcesso.API.Application.Command.AssociarUsuarioPerfil;
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

        /// <summary>
        /// Autenticar usu�rio
        /// </summary>
        /// <param name="autenticarUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("autenticacao")]
        public async Task<IActionResult> AutenticarUsuario(AutenticarUsuarioCommand autenticarUsuarioCommand)
        {
            _logger.LogInformation($"[UsuarioController] Autenticando usu�rio {autenticarUsuarioCommand.Cpf}");
            var resultado = await _mediator.Send(autenticarUsuarioCommand);

            if (resultado.Autenticado)
                return Ok(resultado);

            return Unauthorized(resultado);
        }

        /// <summary>
        /// Autenticar usu�rio
        /// </summary>
        /// <param name="autenticarUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("perfil")]
        public async Task<IActionResult> AssociarUsuarioAoPerfil(AssociarUsuarioPerfilCommand associarUsuarioPerfilCommand)
        {
            _logger.LogInformation($"[UsuarioController] Associando usu�rio {associarUsuarioPerfilCommand.Cpf} ao perfil para o condom�nio {associarUsuarioPerfilCommand.IdCondominio}");
            var resultado = await _mediator.Send(associarUsuarioPerfilCommand);

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }
    }
}