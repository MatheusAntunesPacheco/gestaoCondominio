using Agendamento.API.Infrastructure.EntityConfigurations;
using Agendamento.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Infrastructure
{
    public class AgendamentoContext : DbContext
    {
        public DbSet<AgendamentoModel> Agendamentos { get; set; }
        public DbSet<AuditoriaAgendamentoModel> AuditoriasAgendamento { get; set; }

        public AgendamentoContext(DbContextOptions<AgendamentoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgendamentoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AuditoriaAgendamentosEntityTypeConfiguration());
        }
    }
}
