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
            var resultado = await _mediator.Send(new AgendarEventoCommand(
                            model.IdCondominio,
                            model.IdAreaCondominio,
                            model.Cpf,
                            model.DataEvento,
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
        /// <param name="idCondominio">ID do condominio</param>
        /// <param name="idAreaCondominio">ID da area dentro do condominio</param>
        /// <param name="dataInicio">Data inicio da pesquisa</param>
        /// <param name="dataFim">Data fim da pesquisa</param>
        /// <param name="pagina">Numero da pagina a que se deseja pesquisar</param>
        /// <param name="tamanhoPagina">Tamanha da pagina da pesquisa</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ObterAgendameto(int idCondominio, int idAreaCondominio, DateTime dataInicio, DateTime dataFim, int pagina, int tamanhoPagina)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando consulta por agendamentos do condominio {idCondominio}, area {idAreaCondominio}");
            var consultaPaginadaAgendamentos = _agendamentosRepository.Listar(idCondominio, idAreaCondominio, dataInicio, dataFim, pagina, tamanhoPagina);
            var listaRetornoAgendamentos = consultaPaginadaAgendamentos.listaAgendamentos.Select(a =>
                    new ObterAgendamentoResultado(
                            a.IdCondominio,
                            a.IdAreaCondominio,
                            a.Cpf,
                            a.DataEvento,
                            a.StatusAgendamento.ToString()
                        )
                );

            return Ok(new ConsultaPaginada<ObterAgendamentoResultado>(pagina, tamanhoPagina, consultaPaginadaAgendamentos.quantidadeTotal, listaRetornoAgendamentos));
        }

        /// <summary>
        /// Rota para cancelamento de um agendamento
        /// </summary>
        /// <param name="idCondominio">ID do condominio</param>
        /// <param name="idAreaCondominio">ID da area do condomimio</param>
        /// <param name="dataEvento">Dia do evento</param>
        /// <param name="cpfUsuarioLogado">CPF usuario logado</param>
        /// <returns></returns>
        [HttpPut]
        [Route("cancelamento")]
        public async Task<IActionResult> CancelarAgendamento(int idCondominio, int idAreaCondominio, DateTime dataEvento, string cpfUsuarioLogado)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando cancelamento de agendamento no condominio {idCondominio}, area {idAreaCondominio}, data {dataEvento.ToString("dd-MM-yyyy")}");
            var resultado = await _mediator.Send(new CancelarEventoCommand(
                            idCondominio,
                            idAreaCondominio,
                            dataEvento,
                            cpfUsuarioLogado)
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
        public async Task<IActionResult> ReagendarEvento(int idCondominio, int idAreaCondominio, DateTime dataEvento, string cpfUsuarioLogado, DateTime novaDataEvento)
        {
            _logger.LogInformation($"[AgendamentosController] Iniciando alteração de agendamento no condominio {idCondominio}, area {idAreaCondominio}, data {dataEvento.ToString("dd-MM-yyyy")}");
            var resultado = await _mediator.Send(new AlterarEventoCommand(
                            idCondominio,
                            idAreaCondominio,
                            dataEvento.Date,
                            novaDataEvento.Date,
                            cpfUsuarioLogado)
                        );

            if (resultado.Sucesso)
                return Ok(resultado);

            return BadRequest(resultado);
        }
    }
}