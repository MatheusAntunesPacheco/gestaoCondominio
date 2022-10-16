using Agendamento.API.Infrastructure.Interfaces;
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

            _logger.LogInformation($"[AgendarEventoCommandHandler] Verificando se usuário possui permissão para agendar evento no condominio {request.IdCondominio}");
            if (!request.UsuarioComumCondominio && !request.UsuarioAdministradorCondominio)
                return new ProcessamentoBaseResponse(false, "O usuário logado não tem permissão para agendar eventos neste condomínio");

            _logger.LogInformation($"[AgendarEventoCommandHandler] Verificando se já não existe um agendamento para o condomínio {request.IdCondominio} area {request.IdAreaCondominio} na mesma data");
            var agendamento = _agendamentosRepository.Obter(request.IdCondominio, request.IdAreaCondominio, request.DataEvento);
            if (agendamento != null)
                return new ProcessamentoBaseResponse(false, "Já existe um agendamento para a mesma área na data escolhida");

            _logger.LogInformation($"[AgendarEventoCommandHandler] Realizando agendamento para o condomínio {request.IdCondominio} area {request.IdAreaCondominio}");
            await _agendamentosRepository.Criar(new Entities.Agendamento(request.Cpf, request.IdCondominio, request.IdAreaCondominio, request.DataEvento.Date, request.CpfUsuarioLogado, DateTime.UtcNow));
            return new ProcessamentoBaseResponse(true, string.Empty);
        }
    }
}
