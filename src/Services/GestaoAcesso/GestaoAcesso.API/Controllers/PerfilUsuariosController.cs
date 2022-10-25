using GestaoAcesso.API.Application.Command.AssociarUsuarioPerfil;
using GestaoAcesso.API.Application.Command.DesassociarUsuarioPerfil;
using GestaoAcesso.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoAcesso.Controllers
{
    [ApiController]
    [Route("perfil-usuarios")]
    public class PerfilUsuariosController : ControllerBase
    {
        private readonly ILogger<PerfilUsuariosController> _logger;
        private readonly IMediator _mediator;

        public PerfilUsuariosController(ILogger<PerfilUsuariosController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Associar usuário a um perfil
        /// </summary>
        /// <param name="associarUsuarioPerfilCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AssociarUsuarioAoPerfil(AssociarUsuarioPerfilRequisicao model)
        {
            _logger.LogInformation($"[UsuarioController] Associando usuário {model.Cpf} ao perfil para o condomínio {model.IdCondominio}");

            if (!model.Valido)
                return BadRequest(new { erros = model.Erros });

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

        /// <summary>
        /// Desassociar um perfil de um usuário
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DesassociarUsuarioAoPerfil(DesassociarUsuarioPerfilRequisicao model)
        {
            _logger.LogInformation($"[UsuarioController] Desassociando usuário {model.Cpf} ao perfil para o condomínio {model.IdCondominio}");

            if (!model.Valido)
                return BadRequest(new { erros = model.Erros });

            var resultado = await _mediator.Send(new DesassociarUsuarioPerfilCommand(
                model.Cpf,
                model.IdCondominio,
                model.CpfUsuarioLogado)
            );

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }
    }
}