using Mobile.BFF.API.Application.Command;
using Mobile.BFF.API.Models;
using Mobile.BFF.API.Services.Models;

namespace Mobile.BFF.API.Services
{
    public class GestaoAcessoService : IGestaoAcessoService
    {
        private readonly ILogger<GestaoAcessoService> _logger;
        private readonly IGestaoAcessoClient _gestaoAcessoClient;

        public GestaoAcessoService(ILogger<GestaoAcessoService> logger, IGestaoAcessoClient gestaoAcessoClient)
        {
            _logger = logger;
            _gestaoAcessoClient = gestaoAcessoClient;
        }

        public async Task<ProcessamentoBaseResponse> CriarUsuario(CriacaoUsuariolRequest requisicao)
        {
            _logger.LogInformation($"[GestaoAcessoService] Iniciar criação de novo usuário");
            return await _gestaoAcessoClient.CriarUsuario(requisicao);
        }

        public async Task<AutenticacaoUsuarioResponse> AutenticarUsuario(AutenticacaoUsuarioRequest requisicao)
        {
            _logger.LogInformation($"[GestaoAcessoService] Iniciar verificação de autenticação de usuário");
            return await _gestaoAcessoClient.AutenticarUsuario(requisicao);
        }

        public async Task<ProcessamentoBaseResponse> AssociarUsuarioAUmPerfil(AssociacaoUsuarioPerfilRequest requisicao)
        {
            _logger.LogInformation($"[GestaoAcessoService] Iniciar verificação de associação de usuário");
            return await _gestaoAcessoClient.AssociarUsuarioAUmPerfil(requisicao);
        }

        public async Task<ProcessamentoBaseResponse> DesassociarUsuarioAUmPerfil(DesassociacaoUsuarioPerfilRequest requisicao)
        {
            _logger.LogInformation($"[GestaoAcessoService] Iniciar verificação de desassociação de usuário");
            return await _gestaoAcessoClient.DesassociarUsuarioAUmPerfil(requisicao);
        }
    }
}
