using Agendamento.API.Application.Command.AgendarEvento;
using Agendamento.API.Application.Command.AlterarEvento;
using Agendamento.API.Application.Command.CancelarEvento;
using Agendamento.API.Infrastructure.Interfaces;
using Agendamento.API.Models;
using Agendamento.Infrastructure.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.API.Controllers
{
    [ApiController]
    [Route("agendamentos")]
    public class AgendamentosController : ControllerBase
    {
        private readonly ILogger<AgendamentosController> _logger;
        private readonly IMediator _mediator;
        private readonly IAgendamentosRepository _agendamentosRepository;

        public AgendamentosController(ILogger<AgendamentosController> logger, IMediator mediator,
            IAgendamentosRepository agendamentosRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _agendamentosRepository = agendamentosRepository;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SalvarAgendamento(AgendamentoAreaCondominioModel model)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando agendamento para a area {model.IdAreaCondominio} do condominio {model.IdCondominio} para o usuário {model.Cpf}");
            var resultado = await _mediator.Send(new AgendarEventoCommand(
                            model.Cpf, 
                            model.IdCondominio, 
                            model.IdAreaCondominio, 
                            model.DataEvento, 
                            model.CpfUsuarioLogado, 
                            model.UsuarioAdministradorCondominio, 
                            model.UsuarioComumCondominio)
                        );

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ObterAgendameto(int idCondominio, int idAreaCondominio, DateTime dataInicio, DateTime dataFim, int pagina, int tamanhoPagina)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando consulta por agendamentos do condominio {idCondominio}, area {idAreaCondominio}");
            var consultaPaginadaAgendamentos = _agendamentosRepository.Listar(idCondominio, idAreaCondominio, dataInicio, dataFim, pagina, tamanhoPagina);

            return Ok(new ConsultaPaginada<AgendamentoModel>(pagina, tamanhoPagina, consultaPaginadaAgendamentos.quantidadeTotal, consultaPaginadaAgendamentos.listaAgendamentos));
        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> CancelarAgendamento(AgendamentoAreaCondominioModel model)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando cancelamento de agendamento no condominio {model.IdCondominio}, area {model.IdAreaCondominio}, data {model.DataEvento}");
            var resultado = await _mediator.Send(new CancelarEventoCommand(
                            model.Cpf,
                            model.IdCondominio,
                            model.IdAreaCondominio,
                            model.DataEvento.Date,
                            model.CpfUsuarioLogado,
                            model.UsuarioAdministradorCondominio,
                            model.UsuarioComumCondominio)
                        );

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> AlterarAgendamento(AgendamentoAreaCondominioModel model)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando alteração de agendamento no condominio {model.IdCondominio}, area {model.IdAreaCondominio}, data {model.DataEvento}");
            var resultado = await _mediator.Send(new AlterarEventoCommand(
                            model.Cpf,
                            model.IdCondominio,
                            model.IdAreaCondominio,
                            model.DataEvento.Date,
                            model.CpfUsuarioLogado,
                            model.UsuarioAdministradorCondominio,
                            model.UsuarioComumCondominio)
                        );

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }
    }
}