using Mobile.BFF.API.Application;
using Mobile.BFF.API.Application.Command;
using Mobile.BFF.API.Models;
using Mobile.BFF.API.Services.Models;
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

        public async Task<ProcessamentoBaseResponse> CriarUsuario(CriacaoUsuariolRequest requisicao)
        {
            _logger.LogInformation($"[GestaoAcessoClient] Iniciar requisição HTTP para criação de novo usuário");
            var uri = Configuracao.Url.ApiGestaoAcesso.UrlBasePath + Configuracao.Url.ApiGestaoAcesso.CriacaoUsuario;
            var conteudo = new StringContent(JsonSerializer.Serialize(requisicao), Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync(uri, conteudo);

            return JsonSerializer.Deserialize<ProcessamentoBaseResponse>(await resposta.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<AutenticacaoUsuarioResponse> AutenticarUsuario(AutenticacaoUsuarioRequest requisicao)
        {
            _logger.LogInformation($"[GestaoAcessoClient] Iniciar requisição HTTP para autenticação de usuário");
            var uri = Configuracao.Url.ApiGestaoAcesso.UrlBasePath + Configuracao.Url.ApiGestaoAcesso.AutenticacaoUsuario;
            var conteudo = new StringContent(JsonSerializer.Serialize(requisicao), Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync(uri, conteudo);

            return JsonSerializer.Deserialize<AutenticacaoUsuarioResponse>(await resposta.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<ProcessamentoBaseResponse> AssociarUsuarioAUmPerfil(AssociacaoUsuarioPerfilRequest requisicao)
        {
            _logger.LogInformation($"[GestaoAcessoClient] Iniciar requisição HTTP para associação de usuário");
            var uri = Configuracao.Url.ApiGestaoAcesso.UrlBasePath + Configuracao.Url.ApiGestaoAcesso.AssociacaoUsuario;
            var conteudo = new StringContent(JsonSerializer.Serialize(requisicao), Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync(uri, conteudo);

            return JsonSerializer.Deserialize<ProcessamentoBaseResponse>(await resposta.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<ProcessamentoBaseResponse> DesassociarUsuarioAUmPerfil(DesassociacaoUsuarioPerfilRequest requisicao)
        {
            _logger.LogInformation($"[GestaoAcessoClient] Iniciar requisição HTTP para desassociação de usuário");
            var uri = Configuracao.Url.ApiGestaoAcesso.UrlBasePath + Configuracao.Url.ApiGestaoAcesso.DesassociacaoUsuario;
            var conteudo = new StringContent(JsonSerializer.Serialize(requisicao), Encoding.UTF8, "application/json");
            HttpRequestMessage mensagemHttp = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                Content = conteudo,
                RequestUri = new Uri(uri),
            };
            var resposta = await _httpClient.SendAsync(mensagemHttp);

            return JsonSerializer.Deserialize<ProcessamentoBaseResponse>(await resposta.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
