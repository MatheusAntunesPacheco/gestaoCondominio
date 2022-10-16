using Mobile.BFF.API.Application;
using Mobile.BFF.API.Services.Agendamento.Models;
using System.Text;
using System.Text.Json;

namespace Mobile.BFF.API.Services.Agendamento
{
    public class AgendamentoClient : IAgendamentoClient
    {
        private readonly ILogger<AgendamentoClient> _logger;
        private readonly HttpClient _httpClient;

        public AgendamentoClient(ILogger<AgendamentoClient> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<ProcessamentoBaseResponse> CriarAgendamento(AgendamentoEventoRequest requisicao)
        {
            _logger.LogInformation($"[ApiAgendamento] Iniciar requisição HTTP para agendar um evento");
            var uri = Configuracao.Url.ApiAgendamento.UrlBasePath + Configuracao.Url.ApiAgendamento.AgendarEvento;
            var conteudo = new StringContent(JsonSerializer.Serialize(requisicao), Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync(uri, conteudo);

            return JsonSerializer.Deserialize<ProcessamentoBaseResponse>(await resposta.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}