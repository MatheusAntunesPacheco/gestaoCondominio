using Agendamento.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.API.Infrastructure.EntityConfigurations
{
    public class AuditoriaAgendamentosEntityTypeConfiguration : IEntityTypeConfiguration<AuditoriaAgendamentoModel>
    {
        public void Configure(EntityTypeBuilder<AuditoriaAgendamentoModel> AuditoriaAgendamentoConfiguration)
        {
            AuditoriaAgendamentoConfiguration.ToTable("auditoria_agendamentos");
            AuditoriaAgendamentoConfiguration.HasKey(u => u.Id);
            AuditoriaAgendamentoConfiguration.Property(u => u.IdAgendamento);
            AuditoriaAgendamentoConfiguration.Property(u => u.IdStatus) ;
            AuditoriaAgendamentoConfiguration.Property(u => u.DataEvento);
            AuditoriaAgendamentoConfiguration.Property(u => u.CpfUsuarioAlteracao);
            AuditoriaAgendamentoConfiguration.Property(u => u.DataEvento);
        }
    }
}
