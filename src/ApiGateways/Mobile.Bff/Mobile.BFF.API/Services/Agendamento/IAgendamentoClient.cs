using Mobile.BFF.API.Models.Agendamento;
using Mobile.BFF.API.Services.Agendamento.Models;

namespace Mobile.BFF.API.Services.Agendamento
{
    public interface IAgendamentoClient
    {
        Task<ProcessamentoBaseResponse> CriarAgendamento(AgendamentoEventoRequest requisicao);
        Task<ConsultaPaginada<AgendamentoEventoResult>> ListarAgendamentos(ListarEventoRequest requisicao);
        Task<ProcessamentoBaseResponse> CancelarAgendamento(CancelarEventoRequest requisicao, string usuarioLogado);
    }
}
