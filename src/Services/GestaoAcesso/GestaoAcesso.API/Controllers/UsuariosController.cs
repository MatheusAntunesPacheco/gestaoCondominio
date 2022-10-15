using GestaoAcesso.API.Application.Command.AssociarUsuarioPerfil;
using GestaoAcesso.API.Application.Command.AutenticarUsuario;
using GestaoAcesso.API.Application.Command.CadastrarUsuario;
using GestaoAcesso.API.Models;
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
        /// Cadastrar usuário
        /// </summary>
        /// <param name="cadastrarUsuarioModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CadastrarUsuario(CadastrarUsuarioModel cadastrarUsuarioModel)
        {
            _logger.LogInformation($"[UsuarioController] Cadastrando usuário {cadastrarUsuarioModel.Nome}");
            var resultado = await _mediator.Send(
                new CadastrarUsuarioCommand(
                    cadastrarUsuarioModel.Nome,
                    cadastrarUsuarioModel.Cpf,
                    cadastrarUsuarioModel.Senha,
                    cadastrarUsuarioModel.Email
                )
            );

            return Ok(resultado);
        }

        /// <summary>
        /// Autenticar usuário
        /// </summary>
        /// <param name="autenticarUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("autenticacao")]
        public async Task<IActionResult> AutenticarUsuario(AutenticarUsuarioCommand autenticarUsuarioCommand)
        {
            _logger.LogInformation($"[UsuarioController] Autenticando usuário {autenticarUsuarioCommand.Cpf}");
            var resultado = await _mediator.Send(autenticarUsuarioCommand);

            if (resultado.Autenticado)
                return Ok(resultado);

            return Unauthorized(resultado);
        }

        /// <summary>
        /// Associar usuário a um perfil
        /// </summary>
        /// <param name="associarUsuarioPerfilCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [HttpPost]
        [Route("perfil")]
        public async Task<IActionResult> AssociarUsuarioAoPerfil(AssociarUsuarioPerfilModel model)
        {
            _logger.LogInformation($"[UsuarioController] Associando usuário {model.Cpf} ao perfil para o condomínio {model.IdCondominio}");
            var resultado = await _mediator.Send(new AssociarUsuarioPerfilCommand(
                model.Cpf, 
                model.IdCondominio, 
                model.Administrador,
                model.CpfUsuarioLogado)
            );

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }
    }
}