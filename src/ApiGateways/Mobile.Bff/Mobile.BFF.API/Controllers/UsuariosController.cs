using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobile.BFF.API.Application.Command.AutenticarUsuario;
using Mobile.BFF.API.Application.Command.LerTokenJwt;
using Mobile.BFF.API.Models.GestaoAcesso;
using Mobile.BFF.API.Services.GestaoAcessos;

namespace Mobile.BFF.API.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IMediator _mediator;
        private readonly IGestaoAcessoClient _gestaoAcessoClient;

        public UsuariosController(ILogger<UsuariosController> logger, IMediator mediator, IGestaoAcessoClient gestaoAcessoClient)
        {
            _logger = logger;
            _mediator = mediator;
            _gestaoAcessoClient = gestaoAcessoClient;
        }

        /// <summary>
        /// Cadastrar usuário
        /// </summary>
        /// <param name="cadastrarUsuarioModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CadastrarUsuario(CriacaoUsuariolRequest requisicao)
        {
            _logger.LogInformation($"[UsuarioController] Cadastrando usuário {requisicao.Nome}");
            var resultado = await _gestaoAcessoClient.CriarUsuario(requisicao);

            return Ok(resultado);
        }

        /// <summary>
        /// Rota para autenticar um usuário no sistema e gerar o Tokem JWT
        /// </summary>
        /// <param name="requisicao"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Associar usuário a um perfil
        /// </summary>
        /// <param name="associarUsuarioPerfilCommand"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [HttpPost]
        [Route("perfil")]
        public async Task<IActionResult> AssociarUsuarioAoPerfil([FromHeader] string authorization, AssociarUsuarioPerfilRequest model)
        {
            _logger.LogInformation($"[UsuariosController] Iniciando associação do usuário {model.Cpf}");

            var payloadTokenJwt = await _mediator.Send(new LerPayloadTokenJwtCommand(authorization[7..]));
            var resultado = await _gestaoAcessoClient.AssociarUsuarioAUmPerfil(new Services.GestaoAcessos.Models.AssociacaoUsuarioPerfilRequest(model.Cpf, model.IdCondominio, model.Administrador, payloadTokenJwt.Cpf));
            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }

        [Authorize]
        [HttpDelete]
        [Route("perfil")]
        public async Task<IActionResult> DesassociarUsuarioAoPerfil([FromHeader] string authorization, DesassociarUsuarioPerfilRequest model)
        {
            _logger.LogInformation($"[UsuarioController] Desassociando usuário {model.Cpf} ao perfil para o condomínio {model.IdCondominio}");
            var payloadTokenJwt = await _mediator.Send(new LerPayloadTokenJwtCommand(authorization[7..]));
            var resultado = await _gestaoAcessoClient.DesassociarUsuarioAUmPerfil(new Services.GestaoAcessos.Models.DesassociacaoUsuarioPerfilRequest(model.Cpf, model.IdCondominio, payloadTokenJwt.Cpf));
            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }
    }
}