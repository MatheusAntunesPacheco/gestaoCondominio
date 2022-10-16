using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.API.Infrastructure.EntityConfigurations
{
    public class AgendamentoEntityTypeConfiguration : IEntityTypeConfiguration<Entities.Agendamento>
    {
        public void Configure(EntityTypeBuilder<Entities.Agendamento> usuarioConfiguration)
        {
            usuarioConfiguration.ToTable("agendamentos");
            usuarioConfiguration.HasKey(u => u.Id);
            usuarioConfiguration.Property(u => u.Cpf);
            usuarioConfiguration.Property(u => u.IdCondominio);
            usuarioConfiguration.Property(u => u.IdAreaCondominio);
            usuarioConfiguration.Property(u => u.DataEvento);
            usuarioConfiguration.Property(u => u.CpfUsuarioLogado);
            usuarioConfiguration.Property(u => u.DataAlteracao);
        }
    }
}
