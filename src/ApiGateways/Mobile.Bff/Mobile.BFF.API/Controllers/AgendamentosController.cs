using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobile.BFF.API.Application.Command.LerTokenJwt;
using Mobile.BFF.API.Models;
using Mobile.BFF.API.Models.Agendamento;
using Mobile.BFF.API.Services.Agendamento;
using Mobile.BFF.API.Services.Agendamento.Models;

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

        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListarAgendamentos([FromHeader] string authorization, [FromQuery] ListarEventoRequest requisicao)
        {
            _logger.LogInformation($"[AgendamentoController] Obtendo agendamentos do condominio {requisicao.IdCondominio}, area {requisicao.IdAreaCondominio}");

            var payloadTokenJwt = await _mediator.Send(new LerPayloadTokenJwtCommand(authorization[7..]));

            var resultado = await _agendamentoClient.ListarAgendamentos(requisicao);
            return Ok(resultado);
        }

        /// <summary>
        /// Rota para cancelamento de um agendamento
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("cancelamento")]
        public async Task<IActionResult> CancelarAgendamento([FromHeader] string authorization, CancelarEventoRequest requisicao)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando cancelamento de agendamento no condominio");

            var payloadTokenJwt = await _mediator.Send(new LerPayloadTokenJwtCommand(authorization[7..]));

            var resultado = await _agendamentoClient.CancelarAgendamento(requisicao, payloadTokenJwt.Cpf);

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }
    }
}