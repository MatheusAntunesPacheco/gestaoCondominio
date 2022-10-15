using GestaoAcesso.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoAcesso.API.Infrastructure.EntityConfigurations
{
    public class UsuarioEntityTypeConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> usuarioConfiguration)
        {
            usuarioConfiguration.ToTable("usuarios");
            usuarioConfiguration.HasKey(u => u.Cpf);
            usuarioConfiguration.Property(u => u.Nome);
            usuarioConfiguration.Property(u => u.SenhaCriptografada);
            usuarioConfiguration.Property(u => u.Email);
        }
    }
}
