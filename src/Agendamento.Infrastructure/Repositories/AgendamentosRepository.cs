using Agendamento.API.Infrastructure.Interfaces;
using Agendamento.Infrastructure;
using Agendamento.Infrastructure.Enums;
using Agendamento.Infrastructure.Model;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

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

        public async Task<AgendamentoModel?> Criar(AgendamentoModel agendamento)
        {
            _logger.LogInformation($"Criando um agendamento para o condominio {agendamento.IdCondominio} na area {agendamento.IdAreaCondominio}");
            try
            {
                var agendamentoSalvo = await _context.Agendamentos.AddAsync(agendamento);

                await _context.SaveChangesAsync();

                return agendamentoSalvo.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar agendamento para o condominio {agendamento.IdCondominio} na area {agendamento.IdAreaCondominio}: {ex}");
                return null;
            }
            
        }

        public AgendamentoModel? ObterEventoNaoCancelado(int idCondominio, int idAreaCondominio, DateTime data)
        {
            return _context.Agendamentos.FirstOrDefault(
                                            a => a.IdCondominio == idCondominio
                                         && a.IdAreaCondominio == idAreaCondominio
                                         && a.DataEvento.Date == data.Date
                                         && a.StatusAgendamento != StatusAgendamentoEnum.Cancelado
                                         );
        }

        public (int quantidadeTotal, IEnumerable<AgendamentoModel> listaAgendamentos) Listar(int? idCondominio, int? idAreaCondominio, DateTime? dataInicio, DateTime? dataFim, bool retornarCancelados, int pagina, int tamanhoPagina)
        {
            IQueryable<AgendamentoModel> query = MontarFiltroConsultaAgendamentos(idCondominio, idAreaCondominio, dataInicio, dataFim, retornarCancelados);

            int quantidadeRegistrosAPular = tamanhoPagina * (pagina - 1);
            IQueryable<AgendamentoModel> queryPaginada = query.Skip(quantidadeRegistrosAPular).Take(tamanhoPagina);

            return (query.Count(), queryPaginada);
        }

        private IQueryable<AgendamentoModel> MontarFiltroConsultaAgendamentos(int? idCondominio, int? idAreaCondominio, DateTime? dataInicio, DateTime? dataFim, bool retornarCancelados)
        {
            IQueryable<AgendamentoModel> query = _context.Agendamentos;

            if (idCondominio.HasValue)
                query = query.Where(a => a.IdCondominio == idCondominio.Value);

            if (idAreaCondominio.HasValue)
                query = query.Where(a => a.IdAreaCondominio == idAreaCondominio.Value);

            if (dataInicio.HasValue)
                query = query.Where(a => a.DataEvento >= dataInicio.Value);

            if (dataFim.HasValue)
                query = query.Where(a => a.DataEvento >= dataFim.Value);

            if (!retornarCancelados)
                query = query.Where(a => a.StatusAgendamento != StatusAgendamentoEnum.Cancelado);

            return query;
        }

        public void AtualizarAgendamento(AgendamentoModel agendamento)
        {
            _context.Update(agendamento);
            _context.SaveChanges();
        }
    }
}
