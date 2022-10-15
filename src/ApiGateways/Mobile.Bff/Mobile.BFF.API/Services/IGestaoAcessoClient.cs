using Mobile.BFF.API.Application.Command;
using Mobile.BFF.API.Models;
using Mobile.BFF.API.Services.Models;

namespace Mobile.BFF.API.Services
{
    public interface IGestaoAcessoClient
    {
        Task<ProcessamentoBaseResponse> CriarUsuario(CriacaoUsuariolRequest requisicao);
        Task<AutenticacaoUsuarioResponse> AutenticarUsuario(AutenticacaoUsuarioRequest requisicao);
        Task<ProcessamentoBaseResponse> AssociarUsuarioAUmPerfil(AssociacaoUsuarioPerfilRequest requisicao);
        Task<ProcessamentoBaseResponse> DesassociarUsuarioAUmPerfil(DesassociacaoUsuarioPerfilRequest model);
    }
}
