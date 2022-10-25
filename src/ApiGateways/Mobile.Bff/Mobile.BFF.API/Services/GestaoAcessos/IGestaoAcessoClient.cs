using Mobile.BFF.API.Models;
using Mobile.BFF.API.Services.GestaoAcessos.Models;

namespace Mobile.BFF.API.Services.GestaoAcessos
{
    public interface IGestaoAcessoClient
    {
        Task<ProcessamentoBaseResponse> CriarUsuario(CriacaoUsuariolRequest requisicao);
        Task<AutenticacaoUsuarioResponse> AutenticarUsuario(AutenticacaoUsuarioRequest requisicao);
        Task<ProcessamentoBaseResponse> AssociarUsuarioAUmPerfil(AssociacaoUsuarioPerfilRequest requisicao);
        Task<ProcessamentoBaseResponse> DesassociarUsuarioAUmPerfil(DesassociacaoUsuarioPerfilRequest requisicao);
    }
}
