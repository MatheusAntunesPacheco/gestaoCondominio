using Agendamento.Infrastructure.Model;

namespace Agendamento.API.Infrastructure.Interfaces
{
    public interface IAgendamentosRepository
    {
        Task<AgendamentoModel?> Criar(AgendamentoModel agendamento);
        AgendamentoModel? ObterEventoNaoCancelado(int idCondominio, int idAreaCondominio, DateTime data);
        (int quantidadeTotal, IEnumerable<AgendamentoModel> listaAgendamentos) Listar(int idCondominio, int idAreaCondominio, DateTime dataInicio, DateTime dataFim, int pagina, int tamanhoPagina);
        void AtualizarAgendamento(AgendamentoModel agendamento);
    }
}