using GestaoAcesso.API.Entities;
using GestaoAcesso.API.Infrastructure.Interfaces;

namespace GestaoAcesso.API.Infrastructure.Repositories
{
    public class PerfisUsuariosRepository : IPerfisUsuariosRepository
    {
        private readonly GestaoAcessoContext _context;

        public PerfisUsuariosRepository(GestaoAcessoContext context)
        {
            _context = context;
        }

        public async Task<PerfilUsuario> Criar(PerfilUsuario perfilUsuario)
        {
            var usuarioCadastrado = await _context.PerfisUsuario.AddAsync(perfilUsuario);
            await _context.SaveChangesAsync();

            return usuarioCadastrado.Entity;
        }

        public PerfilUsuario Atualizar(PerfilUsuario perfilUsuario)
        {
            var perfilAtualizado = _context.PerfisUsuario.Update(perfilUsuario).Entity;
            _context.SaveChanges();

            return perfilAtualizado;
        }

        public void Remover(PerfilUsuario perfilUsuario)
        {
            _context.Remove(perfilUsuario);
            _context.SaveChanges();
        }

        public IEnumerable<PerfilUsuario> ListarPorCpf(string cpf) => _context.PerfisUsuario.Where(u => u.Cpf == cpf);
    }
}
