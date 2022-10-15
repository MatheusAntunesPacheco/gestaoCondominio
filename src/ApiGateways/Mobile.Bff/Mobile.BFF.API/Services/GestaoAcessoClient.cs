using Mobile.BFF.API.Config;
using Mobile.BFF.API.Models;
using System.Text;
using System.Text.Json;

namespace Mobile.BFF.API.Services
{
    public class GestaoAcessoClient : IGestaoAcessoClient
    {
        private readonly ILogger<GestaoAcessoClient> _logger;
        private readonly HttpClient _httpClient;

        public GestaoAcessoClient(ILogger<GestaoAcessoClient> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<AutenticacaoUsuarioResponse> AutenticarUsuario(AutenticacaoUsuarioRequest requisicao)
        {
            _logger.LogInformation($"[GestaoAcessoClient] Iniciar requisição HTTP para autenticação de usuário");
            var uri = UrlsConfig.GestaoAcesso.UrlBasePath + UrlsConfig.GestaoAcesso.AutenticarUsuario;
            var conteudo = new StringContent(JsonSerializer.Serialize(requisicao), Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync(uri, conteudo);

            var tokenAutenticacao = await resposta.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<AutenticacaoUsuarioResponse>(tokenAutenticacao, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
