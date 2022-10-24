using GestaoAcesso.API.Application.Command.AssociarUsuarioPerfil;
using GestaoAcesso.API.Application.Command.AutenticarUsuario;
using GestaoAcesso.API.Application.Command.CadastrarUsuario;
using GestaoAcesso.API.Application.Command.DesassociarUsuarioPerfil;
using GestaoAcesso.API.Application.Command.ObterDadosUsuario;
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
        /// Cadastrar usu�rio
        /// </summary>
        /// <param name="cadastrarUsuarioModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CadastrarUsuario(CadastrarUsuarioRequisicao model)
        {
            _logger.LogInformation($"[UsuarioController] Cadastrando usu�rio {model.Nome}");

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
        /// Autenticar usu�rio
        /// </summary>
        /// <param name="autenticarUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("autenticacao")]
        public async Task<IActionResult> AutenticarUsuario(AutenticarUsuarioRequisicao model)
        {
            _logger.LogInformation($"[UsuarioController] Autenticando usu�rio");

            if (!model.Valido)
                return BadRequest(new { erros = model.Erros });

            var resultado = await _mediator.Send(new AutenticarUsuarioCommand(model.Cpf, model.Senha));

            if (resultado.Autenticado)
                return Ok(resultado);

            return Unauthorized(resultado);
        }

        /// <summary>
        /// Associar usu�rio a um perfil
        /// </summary>
        /// <param name="associarUsuarioPerfilCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [HttpPost]
        [Route("perfil")]
        public async Task<IActionResult> AssociarUsuarioAoPerfil(AssociarUsuarioPerfilRequisicao model)
        {
            _logger.LogInformation($"[UsuarioController] Associando usu�rio {model.Cpf} ao perfil para o condom�nio {model.IdCondominio}");

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
        /// Desassociar um perfil de um usu�rio
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("perfil")]
        public async Task<IActionResult> DesassociarUsuarioAoPerfil(DesassociarUsuarioPerfilRequisicao model)
        {
            _logger.LogInformation($"[UsuarioController] Desassociando usu�rio {model.Cpf} ao perfil para o condom�nio {model.IdCondominio}");

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

        /// <summary>
        /// Verifica se usu�rio � administrador do condominio
        /// </summary>
        /// <param name="idCondominio"></param>
        /// <param name="cpfUsuario"></param>
        /// <returns></returns>
        [HttpHead]
        [Route("administrador")]
        public async Task<IActionResult> VerificarSeUsuarioEhAdministradorDoCondominio(int idCondominio, string cpfUsuario)
        {
            _logger.LogInformation($"[UsuarioController] Verificando se o usu�rio {cpfUsuario} � administrador do condom�nio {idCondominio}");

            var resultado = await _mediator.Send(new ObterDadosUsuarioCommand(cpfUsuario));

            if (resultado == null || !resultado.ListaPerfis.Any())
                return Unauthorized();

            if (resultado.ListaPerfis.Any(p => p.PerfilAdministradorCondominio(idCondominio)))
                return Ok();

            return Unauthorized();
        }

        /// <summary>
        /// Verifica se usu�rio � vinculado ao condominio
        /// </summary>
        /// <param name="idCondominio"></param>
        /// <param name="cpfUsuario"></param>
        /// <returns></returns>
        [HttpHead]
        [Route("morador")]
        public async Task<IActionResult> VerificarSeUsuarioEstaVinculadoAoCondominio(int idCondominio, string cpfUsuario)
        {
            _logger.LogInformation($"[UsuarioController] Verificando se o usu�rio {cpfUsuario} � administrador do condom�nio {idCondominio}");

            var resultado = await _mediator.Send(new ObterDadosUsuarioCommand(cpfUsuario));

            if (resultado == null || !resultado.ListaPerfis.Any())
                return Unauthorized();

            if (resultado.ListaPerfis.Any(p => p.PerfilAdministradorGeral || p.IdCondominio == idCondominio))
                return Ok();

            return Unauthorized();
        }
    }
}