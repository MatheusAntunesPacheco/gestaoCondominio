using Agendamento.Infrastructure.Interfaces.Repositories;
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
            if (request.DataAtualEvento == request.NovaDataEvento)
                return new ProcessamentoBaseResponse(false, "Evento não será reagendado pois a data informada é a mesma data já agendada");

            _logger.LogInformation($"[AlterarEventoCommandHandler] Verificando se existe o agendamento para o condomínio {request.IdCondominio} area {request.IdAreaCondominio} na mesma data");
            var agendamento = _agendamentosRepository.ObterEventoNaoCancelado(request.IdCondominio, request.IdAreaCondominio, request.DataAtualEvento);
            if (agendamento == null)
                return new ProcessamentoBaseResponse(false, "Não existe o agendamento vigente para a área na data escolhida. Não foi possível alterar o evento");

            _logger.LogInformation($"[AlterarEventoCommandHandler] Verificando se já não existe um agendamento para o condomínio {request.IdCondominio} area {request.IdAreaCondominio} na nova data escolhida");
            var agendamentoNovaData = _agendamentosRepository.ObterEventoNaoCancelado(request.IdCondominio, request.IdAreaCondominio, request.NovaDataEvento);
            if (agendamentoNovaData != null)
                return new ProcessamentoBaseResponse(false, "Para a nova data selecionada já existe um agendamento vigente para a área na data escolhida. Não foi possível alterar o evento");

            _logger.LogInformation($"[AlterarEventoCommandHandler] Alterando agendamento para o condomínio {request.IdCondominio} area {request.IdAreaCondominio}");
            agendamento.AlterarDataEvento(request.CpfUsuarioLogado, request.NovaDataEvento);
            _agendamentosRepository.AtualizarAgendamento(agendamento);

            return new ProcessamentoBaseResponse(true, string.Empty);
        }
    }
}
