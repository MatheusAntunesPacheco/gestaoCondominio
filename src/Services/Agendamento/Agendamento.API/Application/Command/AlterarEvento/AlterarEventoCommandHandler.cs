using Agendamento.API.Infrastructure.Interfaces;
using Agendamento.Domain;
using MediatR;

namespace Agendamento.API.Application.Command.AlterarEvento
{
    public class AlterarEventoCommandHandler : IRequestHandler<AlterarEventoCommand, ProcessamentoBaseResponse>
    {
        private readonly ILogger<AlterarEventoCommandHandler> _logger;
        private readonly IAgendamentosRepository _agendamentosRepository;

        public AlterarEventoCommandHandler(ILogger<AlterarEventoCommandHandler> logger, IAgendamentosRepository agendamentosRepository)
        {
            _logger = logger;
            _agendamentosRepository = agendamentosRepository;
        }
        public async Task<ProcessamentoBaseResponse> Handle(AlterarEventoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[AlterarEventoCommandHandler] Iniciando alteração de um evento no condomínio {request.IdCondominio} area {request.IdAreaCondominio}");

            _logger.LogInformation($"[AlterarEventoCommandHandler] Verificando se existe o agendamento para o condomínio {request.IdCondominio} area {request.IdAreaCondominio} na mesma data");
            var agendamento = _agendamentosRepository.Obter(request.IdCondominio, request.IdAreaCondominio, request.DataEvento);
            if (agendamento == null)
                return new ProcessamentoBaseResponse(false, "Não existe o agendamento para a área na data escolhida. Não foi possível alterar o evento");

            _logger.LogInformation($"[AlterarEventoCommandHandler] Verificando se usuário possui permissão para alterar evento no condominio {request.IdCondominio}");
            if (!request.UsuarioAdministradorCondominio && agendamento.Cpf == request.CpfUsuarioLogado)
                return new ProcessamentoBaseResponse(false, "O usuário logado não tem permissão para alterar o evento indicado");

            _logger.LogInformation($"[AlterarEventoCommandHandler] Alterando agendamento para o condomínio {request.IdCondominio} area {request.IdAreaCondominio}");
            agendamento.AlterarDataEvento(request.CpfUsuarioLogado, request.DataEvento);
            _agendamentosRepository.AtualizarAgendamento(agendamento);
            return new ProcessamentoBaseResponse(true, string.Empty);
        }
    }
}
