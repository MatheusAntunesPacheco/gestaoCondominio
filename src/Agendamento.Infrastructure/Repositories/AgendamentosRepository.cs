using Agendamento.API.Infrastructure.Interfaces;
using Agendamento.Domain;
using Agendamento.Infrastructure;
using Agendamento.Infrastructure.Model;
using Microsoft.Extensions.Logging;

namespace Agendamento.API.Infrastructure.Repositories
{
    public class AgendamentosRepository : IAgendamentosRepository
    {
        private readonly AgendamentoContext _context;
        private readonly ILogger<AgendamentosRepository> _logger;

        public AgendamentosRepository(AgendamentoContext context, ILogger<AgendamentosRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AgendamentoModel> Criar(AgendamentoDomain agendamento)
        {
            _logger.LogInformation($"Criando um agendamento para o condominio {agendamento.IdCondominio} na area {agendamento.IdAreaCondominio}");
            var agendamentoModelDb = new AgendamentoModel(
                    agendamento.Cpf,
                    agendamento.IdCondominio,
                    agendamento.IdAreaCondominio,
                    agendamento.DataEvento,
                    (int)StatusAgendamentoEnum.Agendado,
                    DateTime.UtcNow,
                    agendamento.CpfUsuarioLogado
                );
            try
            {
                var agendamentoSalvo = await _context.Agendamentos.AddAsync(agendamentoModelDb);

                await _context.SaveChangesAsync();

                return agendamentoSalvo.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar agendamento para o condominio {agendamento.IdCondominio} na area {agendamento.IdAreaCondominio}: {ex}");
                return null;
            }
            
        }

        public AgendamentoModel Obter(int idCondominio, int idAreaCondominio, DateTime data)
        {
            return _context.Agendamentos.FirstOrDefault(
                                            a => a.IdCondominio == idCondominio
                                         && a.IdAreaCondominio == idAreaCondominio
                                         && a.DataEvento.Date == data.Date
                                         );
        }

        public (int quantidadeTotal, IEnumerable<AgendamentoModel> listaAgendamentos) Listar(int idCondominio, int idAreaCondominio, DateTime dataInicio, DateTime dataFim, int pagina, int tamanhoPagina)
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

        public void AtualizarAgendamento(AgendamentoModel agendamento)
        {
            _context.Update(agendamento);
            _context.SaveChanges();
        }
    }
}
