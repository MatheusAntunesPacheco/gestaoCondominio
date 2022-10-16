using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobile.BFF.API.Application.Command.LerTokenJwt;
using Mobile.BFF.API.Models;
using Mobile.BFF.API.Services.Agendamento;
using Mobile.BFF.API.Services.Agendamento.Models;
using Mobile.BFF.API.Services.GestaoAcessos;

namespace Mobile.BFF.API.Controllers
{
    [ApiController]
    [Route("agendamentos")]
    public class AgendamentosController : ControllerBase
    {
        private readonly ILogger<AgendamentosController> _logger;
        private readonly IMediator _mediator;
        private readonly IAgendamentoClient _agendamentoClient;

        public AgendamentosController(ILogger<AgendamentosController> logger, IMediator mediator, IAgendamentoClient agendamentoClient)
        {
            _logger = logger;
            _mediator = mediator;
            _agendamentoClient = agendamentoClient;
        }

        /// <summary>
        /// Cadastrar usuário
        /// </summary>
        /// <param name="cadastrarUsuarioModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AgendarEvento([FromHeader] string authorization, AgendarEventoRequest requisicao)
        {
            _logger.LogInformation($"[AgendamentosController] Agendando evento no condomínio {requisicao.IdCondominio}, area {requisicao.IdAreaCondominio}");
            var payloadTokenJwt = await _mediator.Send(new LerPayloadTokenJwtCommand(authorization[7..]));

            var resultado = await _agendamentoClient.CriarAgendamento(new AgendamentoEventoRequest(
                                                                            requisicao.Cpf,
                                                                            requisicao.IdCondominio,
                                                                            requisicao.IdAreaCondominio,
                                                                            requisicao.DataEvento.Date,
                                                                            payloadTokenJwt.Cpf,
                                                                            payloadTokenJwt.UsuarioEhAdministradorCondominio(requisicao.IdCondominio),
                                                                            payloadTokenJwt.UsuarioApenasUsuarioComum(requisicao.IdCondominio)
                                                                            ));

            return Ok(resultado);
        }
    }
}