using Mobile.BFF.API.Services.Agendamento.Models;

namespace Mobile.BFF.API.Services.Agendamento
{
    public interface IAgendamentoClient
    {
        Task<ProcessamentoBaseResponse> CriarAgendamento(AgendamentoEventoRequest requisicao);
        Task<ConsultaPaginada<AgendamentoEventoResult>> ListarAgendamentos(int idCondominio, int idAreaCondominio, DateTime dataInicio, DateTime dataFim, int pagina, int tamanhoPagina);
    }
}
