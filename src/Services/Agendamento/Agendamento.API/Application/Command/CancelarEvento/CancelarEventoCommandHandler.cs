﻿using Agendamento.API.Infrastructure.Interfaces;
using Agendamento.Domain;
using MediatR;

namespace Agendamento.API.Application.Command.CancelarEvento
{
    public class CancelarEventoCommandHandler : IRequestHandler<CancelarEventoCommand, ProcessamentoBaseResponse>
    {
        private readonly ILogger<CancelarEventoCommandHandler> _logger;
        private readonly IAgendamentosRepository _agendamentosRepository;

        public CancelarEventoCommandHandler(ILogger<CancelarEventoCommandHandler> logger, IAgendamentosRepository agendamentosRepository)
        {
            _logger = logger;
            _agendamentosRepository = agendamentosRepository;
        }
        public async Task<ProcessamentoBaseResponse> Handle(CancelarEventoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[CancelarEventoCommandHandler] Iniciando agendamento de um evento no condomínio {request.IdCondominio} area {request.IdAreaCondominio}");

            _logger.LogInformation($"[CancelarEventoCommandHandler] Verificando se existe o agendamento para o condomínio {request.IdCondominio} area {request.IdAreaCondominio} na mesma data");
            var agendamento = _agendamentosRepository.Obter(request.IdCondominio, request.IdAreaCondominio, request.DataEvento);
            if (agendamento == null)
                return new ProcessamentoBaseResponse(false, "Não existe agendamento na data indicada. Não foi possível cancelar o evento");

            _logger.LogInformation($"[CancelarEventoCommandHandler] Verificando se usuário possui permissão para cancelar evento no condominio {request.IdCondominio}");
            if (!request.UsuarioAdministradorCondominio && agendamento.Cpf != request.CpfUsuarioLogado)
                return new ProcessamentoBaseResponse(false, "O usuário logado não tem permissão para cancelar o evento solicitado");

            _logger.LogInformation($"[CancelarEventoCommandHandler] Realizando cancelamento do evento para o condomínio {request.IdCondominio} area {request.IdAreaCondominio}");
            agendamento.AlterarStatusAgendamento(request.CpfUsuarioLogado, (int)StatusAgendamentoEnum.Cancelado);
            _agendamentosRepository.AtualizarAgendamento(agendamento);
            
            return new ProcessamentoBaseResponse(true, string.Empty);
        }
    }
}
