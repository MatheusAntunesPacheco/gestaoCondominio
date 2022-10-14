using Mobile.BFF.API.Models;

namespace Mobile.BFF.API.Services
{
    public interface IGestaoAcessoService
    {
        Task<AutenticacaoUsuarioResponse> AutenticarUsuario(AutenticacaoUsuarioRequest requisicao);
    }
}
