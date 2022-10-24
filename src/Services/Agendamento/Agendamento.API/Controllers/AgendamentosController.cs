using Agendamento.API.Application.Command.AgendarEvento;
using Agendamento.API.Application.Command.AlterarEvento;
using Agendamento.API.Application.Command.CancelarEvento;
using Agendamento.API.Infrastructure.Interfaces;
using Agendamento.API.Models;
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

        /// <summary>
        /// Rota para agendar uma area do condominio em um determinado dia
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SalvarAgendamento(CriarAgendamentoRequisicao model)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando agendamento para a area {model.IdAreaCondominio} do condominio {model.IdCondominio} para o usuário {model.Cpf}");

            if(!model.Valido)
                return BadRequest(new { erros = model.Erros });

            var resultado = await _mediator.Send(new AgendarEventoCommand(
                            model.IdCondominio.Value,
                            model.IdAreaCondominio.Value,
                            model.Cpf,
                            model.DataEvento.Value.Date,
                            model.CpfUsuarioLogado)
                        );

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }

        /// <summary>
        /// Rota para consulta de agendamentos conforme parametros informados
        /// Retorno se trata de uma consulta paginada
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ObterAgendameto([FromQuery] ObterAgendamentoRequisicao model)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando consulta por agendamentos do condominio");

            if(!model.Valido)
                return BadRequest(new { erros = model.Erros});

            var consultaPaginadaAgendamentos = _agendamentosRepository.Listar(
                            model.IdCondominio, 
                            model.IdAreaCondominio, 
                            model.DataInicio, 
                            model.DataFim, 
                            model.ConsultarAgendamentosCancelados, 
                            model.Pagina, 
                            model.TamanhoPagina
               );

            var listaRetornoAgendamentos = consultaPaginadaAgendamentos.listaAgendamentos.Select(a =>
                    new ObterAgendamentoResultado(
                            a.IdCondominio,
                            a.IdAreaCondominio,
                            a.Cpf,
                            a.DataEvento,
                            a.StatusAgendamento.ToString()
                        )
                );

            return Ok(new ConsultaPaginada<ObterAgendamentoResultado>(model.Pagina, model.TamanhoPagina, consultaPaginadaAgendamentos.quantidadeTotal, listaRetornoAgendamentos));
        }

        /// <summary>
        /// Rota para cancelamento de um agendamento
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("cancelamento")]
        public async Task<IActionResult> CancelarAgendamento([FromQuery] CancelarAgendamentoRequisicao model)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando cancelamento de agendamento no condominio");

            if (!model.Valido)
                return BadRequest(new { erros = model.Erros });

            var resultado = await _mediator.Send(new CancelarEventoCommand(
                            model.IdCondominio.Value,
                            model.IdAreaCondominio.Value,
                            model.DataEvento.Value,
                            model.CpfUsuarioLogado)
                        );

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }

        /// <summary>
        /// Reagendar um evento
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("reagendamento")]
        public async Task<IActionResult> ReagendarEvento([FromQuery] AlterarAgendamentoRequisicao model)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando alteração de agendamento");

            if (!model.Valido)
                return BadRequest(new { erros = model.Erros });

            var resultado = await _mediator.Send(new AlterarEventoCommand(
                            model.IdCondominio.Value,
                            model.IdAreaCondominio.Value,
                            model.DataAtualEvento.Value.Date,
                            model.NovaDataEvento.Value.Date,
                            model.CpfUsuarioLogado)
                        );

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }
    }
}