using GestaoAcesso.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoAcesso.API.Infrastructure.EntityConfigurations
{
    public class UsuarioPerfilEntityTypeConfiguration : IEntityTypeConfiguration<PerfilUsuario>
    {
        public void Configure(EntityTypeBuilder<PerfilUsuario> usuarioConfiguration)
        {
            usuarioConfiguration.ToTable("perfis_usuarios");
            usuarioConfiguration.HasKey(u => u.Id);
            usuarioConfiguration.Property(u => u.Cpf);
            usuarioConfiguration.Property(u => u.IdCondominio);
            usuarioConfiguration.Property(u => u.Administrador);
        }
    }
}
