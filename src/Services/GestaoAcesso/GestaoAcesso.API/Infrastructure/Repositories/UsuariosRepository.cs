using GestaoAcesso.API.Entities;
using GestaoAcesso.API.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoAcesso.API.Infrastructure.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly GestaoAcessoContext _context;

        public UsuariosRepository(GestaoAcessoContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Criar(Usuario usuario)
        {
            var usuarioCadastrado = await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuarioCadastrado.Entity;
        }

        public async Task<Usuario> ObterPorCpf(string cpf) => await _context.Usuarios.Where(u => u.Cpf == cpf).FirstOrDefaultAsync();
    }
}
