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
        /// <param name="cadastrarUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CadastrarUsuario(CadastrarUsuarioCommand cadastrarUsuarioCommand)
        {
            _logger.LogInformation($"[UsuarioController] Cadastrando usuário {cadastrarUsuarioCommand.Nome}");
            var resultado = await _mediator.Send(cadastrarUsuarioCommand);

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
        //[HttpPut]
        [HttpPost]
        [Route("perfil")]
        public async Task<IActionResult> AssociarUsuarioAoPerfil(AssociarUsuarioPerfilModel model)
        {
            var toke = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJEYWRvc1VzdWFyaW8iOnsiQ3BmIjoiMDc2NjY5NjU2MTMiLCJOb21lIjoiTWF0aGV1cyBQYWNoZWNvIiwiQWRtaW5pc3RyYWRvckdlcmFsIjp0cnVlLCJDb25kb21pbmlvc0FkbWluaXN0cmFkb3IiOltdLCJDb25kb21pbmlvc1VzdWFyaW9Db211bSI6W119LCJuYmYiOjE2NjU4NDc1OTgsImV4cCI6MTY2NTkzMzk5OCwiaWF0IjoxNjY1ODQ3NTk4LCJpc3MiOiJhdXRlbnRpY2FjYW8tdXN1YXJpb3MiLCJhdWQiOiJnZXN0YW8tYWNlc3NvcyJ9.MDnQcUAVs2pXns6BeORi98NjvNhjzDVaQkddbqbWUmo";
            _logger.LogInformation($"[UsuarioController] Associando usuário {model.Cpf} ao perfil para o condomínio {model.IdCondominio}");
            var resultado = await _mediator.Send(new AssociarUsuarioPerfilCommand(
                model.Cpf, 
                model.IdCondominio, 
                model.Administrador,
                toke)
            );

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }
    }
}