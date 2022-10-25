using Agendamento.Infrastructure.Enums;
using Agendamento.Infrastructure.Interfaces.Repositories;
using Agendamento.Infrastructure.Model;
using MediatR;

namespace Agendamento.API.Application.Command.AgendarEvento
{
    public class AgendarEventoCommandHandler : IRequestHandler<AgendarEventoCommand, ProcessamentoBaseResponse>
    {
        private readonly ILogger<AgendarEventoCommandHandler> _logger;
        private readonly IAgendamentosRepository _agendamentosRepository;

        public AgendarEventoCommandHandler(ILogger<AgendarEventoCommandHandler> logger, IAgendamentosRepository agendamentosRepository)
        {
            _logger = logger;
            _agendamentosRepository = agendamentosRepository;
        }
        public async Task<ProcessamentoBaseResponse> Handle(AgendarEventoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[AgendarEventoCommandHandler] Iniciando agendamento de um evento no condomínio {request.IdCondominio} area {request.IdAreaCondominio}");

            _logger.LogInformation($"[AgendarEventoCommandHandler] Verificando se já não existe um agendamento para o condomínio {request.IdCondominio} area {request.IdAreaCondominio} na mesma data");
            var agendamento = _agendamentosRepository.ObterEventoNaoCancelado(request.IdCondominio, request.IdAreaCondominio, request.DataEvento);
            if (agendamento != null)
                return new ProcessamentoBaseResponse(false, "Já existe um agendamento para a mesma área na data escolhida");

            _logger.LogInformation($"[AgendarEventoCommandHandler] Realizando agendamento para o condomínio {request.IdCondominio} area {request.IdAreaCondominio}");
            await _agendamentosRepository.Criar(new AgendamentoModel(
                            request.Cpf,
                            request.IdCondominio,
                            request.IdAreaCondominio,
                            request.DataEvento.Date,
                            StatusAgendamentoEnum.Agendado,
                            DateTime.UtcNow,
                            request.CpfUsuarioLogado
                        )
                );

            return new ProcessamentoBaseResponse(true, string.Empty);
        }
    }
}
