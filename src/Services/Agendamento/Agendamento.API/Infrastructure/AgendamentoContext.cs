using Agendamento.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.API.Infrastructure
{
    public class AgendamentoContext : DbContext
    {
        public DbSet<Entities.Agendamento> Agendamentos { get; set; }

        public AgendamentoContext(DbContextOptions<AgendamentoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgendamentoEntityTypeConfiguration());
        }
    }
}
