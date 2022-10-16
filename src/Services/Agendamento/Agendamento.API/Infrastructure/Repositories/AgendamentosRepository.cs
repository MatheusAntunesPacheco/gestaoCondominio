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
    }
}
