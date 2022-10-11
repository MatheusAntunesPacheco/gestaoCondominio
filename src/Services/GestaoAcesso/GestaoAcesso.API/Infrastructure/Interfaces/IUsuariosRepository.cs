using GestaoAcesso.API.Entities;

namespace GestaoAcesso.API.Infrastructure.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<Usuario> Criar(Usuario usuario);
        Task<Usuario> ObterPorCpf(string cpf);
    }
}