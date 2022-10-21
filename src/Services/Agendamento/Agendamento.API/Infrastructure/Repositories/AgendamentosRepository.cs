using Agendamento.API.Infrastructure.Interfaces;

namespace Agendamento.API.Infrastructure.Repositories
{
    public class AgendamentosRepository : IAgendamentosRepository
    {
        private readonly AgendamentoContext _context;

        public AgendamentosRepository(AgendamentoContext context)
        {
            _context = context;
        }

        public async Task<Entities.Agendamento> Criar(Entities.Agendamento agendamento)
        {
            var agendamentoSalvo = await _context.Agendamentos.AddAsync(agendamento);
            await _context.SaveChangesAsync();

            return agendamentoSalvo.Entity;
        }

        public Entities.Agendamento Obter(int idCondominio, int idAreaCondominio, DateTime data)
        {
            return _context.Agendamentos.FirstOrDefault(
                                            a => a.IdCondominio == idCondominio
                                         && a.IdAreaCondominio == idAreaCondominio
                                         && a.DataEvento.Date == data.Date
                                         );
        }

        public (int quantidadeTotal, IEnumerable<Entities.Agendamento> listaAgendamentos) Listar(int idCondominio, int idAreaCondominio, DateTime dataInicio, DateTime dataFim, int pagina, int tamanhoPagina)
        {
            int quantidadeRegistrosAPular = tamanhoPagina * (pagina - 1);
            var agendamentos = _context.Agendamentos.Where(
                        a => a.IdCondominio == idCondominio
                          && a.IdAreaCondominio == idAreaCondominio
                          && a.DataEvento >= dataInicio
                          && a.DataEvento <= dataFim);

            var paginaDeDados = agendamentos
                        .Skip(quantidadeRegistrosAPular).Take(tamanhoPagina);

            return (agendamentos.Count(), paginaDeDados.Select(e => e));
        }
    }
}
