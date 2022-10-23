using Agendamento.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.API.Infrastructure.EntityConfigurations
{
    public class AgendamentoEntityTypeConfiguration : IEntityTypeConfiguration<AgendamentoModel>
    {
        public void Configure(EntityTypeBuilder<AgendamentoModel> agendamentoConfiguration)
        {
            agendamentoConfiguration.ToTable("agendamentos");
            agendamentoConfiguration.HasKey(u => u.Id);
            agendamentoConfiguration.Property(u => u.Cpf);
            agendamentoConfiguration.Property(u => u.IdCondominio);
            agendamentoConfiguration.Property(u => u.IdAreaCondominio);
            agendamentoConfiguration.Property(u => u.DataEvento);
            agendamentoConfiguration.Property(u => u.StatusAgendamento);
            agendamentoConfiguration.Property(u => u.DataAlteracao);
        }
    }
}
