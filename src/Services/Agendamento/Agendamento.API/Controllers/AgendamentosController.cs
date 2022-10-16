using Agendamento.API.Application.Command.AgendarEvento;
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

        public AgendamentosController(ILogger<AgendamentosController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SalvarAgendamento(AgendamentoAreaCondominioModel model)
        {
            _logger.LogInformation($"Iniciando agendamento para a area {model.IdAreaCondominio} do condominio {model.IdCondominio} para o usuário {model.Cpf}");
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
    }
}