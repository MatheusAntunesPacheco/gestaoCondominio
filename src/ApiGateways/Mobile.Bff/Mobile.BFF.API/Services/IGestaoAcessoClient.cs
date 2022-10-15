using Mobile.BFF.API.Models;

namespace Mobile.BFF.API.Services
{
    public interface IGestaoAcessoClient
    {
        Task<AutenticacaoUsuarioResponse> AutenticarUsuario(AutenticacaoUsuarioRequest requisicao);
    }
}
