using Mobile.BFF.API.Services.Agendamento.Models;

namespace Mobile.BFF.API.Services.Agendamento
{
    public interface IAgendamentoClient
    {
        Task<ProcessamentoBaseResponse> CriarAgendamento(AgendamentoEventoRequest requisicao);
    }
}
