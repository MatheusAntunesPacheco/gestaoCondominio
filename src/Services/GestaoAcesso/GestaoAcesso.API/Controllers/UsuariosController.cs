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
        public async Task<IActionResult> CadastrarUsuario(CadastrarUsuarioRequisicao model)
        {
            _logger.LogInformation($"[UsuarioController] Cadastrando usuário {model.Nome}");

            if (!model.Valido)
                return BadRequest(new { erros = model.Erros });

            var resultado = await _mediator.Send(
                new CadastrarUsuarioCommand(
                    model.Nome,
                    model.Cpf,
                    model.Senha,
                    model.Email
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
        public async Task<IActionResult> AutenticarUsuario(AutenticarUsuarioRequisicao model)
        {
            _logger.LogInformation($"[UsuarioController] Autenticando usuário");

            if (!model.Valido)
                return BadRequest(new { erros = model.Erros });

            var resultado = await _mediator.Send(new AutenticarUsuarioCommand(model.Cpf, model.Senha));

            if (resultado.Autenticado)
                return Ok(resultado);

            return Unauthorized(resultado);
        }
    }
}