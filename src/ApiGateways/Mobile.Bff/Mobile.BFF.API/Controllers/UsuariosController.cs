using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobile.BFF.API.Application.Command.AutenticarUsuario;
using Mobile.BFF.API.Application.Command.LerTokenJwt;
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
        private readonly IGestaoAcessoService _gestaoAcessoService;

        public UsuariosController(ILogger<UsuariosController> logger, IMediator mediator, IGestaoAcessoService gestaoAcessoService)
        {
            _logger = logger;
            _mediator = mediator;
            _gestaoAcessoService = gestaoAcessoService;
        }

        [HttpPost()]
        [Route("autenticacao")]
        public async Task<IActionResult> AutenticarUsuario([FromBody] AutenticarUsuarioCommand requisicao)
        {
            _logger.LogInformation($"[UsuariosController] Iniciando autentica��o do usu�rio {requisicao.Cpf}");
            var resultadoAutenticacao = await _mediator.Send(requisicao);

            if (resultadoAutenticacao.Autenticado)
                return Ok(resultadoAutenticacao);

            return Unauthorized(resultadoAutenticacao);
        }

        /// <summary>
        /// Associar usu�rio a um perfil
        /// </summary>
        /// <param name="associarUsuarioPerfilCommand"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [HttpPost]
        [Route("perfil")]
        public async Task<IActionResult> AssociarUsuarioAoPerfil([FromHeader] string authorization, AssociarUsuarioPerfilRequest model)
        {
            _logger.LogInformation($"[UsuariosController] Iniciando associa��o do usu�rio {model.Cpf}");

            var payloadTokenJwt = await _mediator.Send(new LerPayloadTokenJwtCommand(authorization[7..]));
            var resultado = await _gestaoAcessoService.AssociarUsuarioAUmPerfil(new Services.Models.AssociacaoUsuarioPerfilRequest(model.Cpf, model.IdCondominio, model.Administrador, payloadTokenJwt.Cpf));
            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }
    }
}