using GestaoAcesso.API.Entities;
using GestaoAcesso.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace GestaoAcesso.API.Infrastructure
{
    public class GestaoAcessoContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PerfilUsuario> PerfisUsuario { get; set; }

        public GestaoAcessoContext(DbContextOptions<GestaoAcessoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioPerfilEntityTypeConfiguration());
        }
    }
}
