using Mobile.BFF.API.Models;

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

        public async Task<AutenticacaoUsuarioResponse> AutenticarUsuario(AutenticacaoUsuarioRequest requisicao)
        {
            _logger.LogInformation($"[GestaoAcessoService] Iniciar verificação de autenticação de usuário");
            return await _gestaoAcessoClient.AutenticarUsuario(requisicao);
        }
    }
}
