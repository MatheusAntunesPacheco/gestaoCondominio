using Mobile.BFF.API.Application;
using Mobile.BFF.API.Models.Agendamento;
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
            _logger.LogInformation($"[AgendamentoClient] Iniciar requisição HTTP para agendar um evento");
            var uri = Configuracao.Url.ApiAgendamento.UrlBasePath + Configuracao.Url.ApiAgendamento.AgendarEvento;
            var conteudo = new StringContent(JsonSerializer.Serialize(requisicao), Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PostAsync(uri, conteudo);

            return JsonSerializer.Deserialize<ProcessamentoBaseResponse>(await resposta.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<ConsultaPaginada<AgendamentoEventoResult>> ListarAgendamentos(ListarEventoRequest requisicao)
        {
            _logger.LogInformation($"[AgendamentoClient] Iniciar requisição HTTP para listar agendamento um evento");
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("idCondominio", requisicao.IdCondominio?.ToString());
            queryString.Add("idAreaCondominio", requisicao.IdAreaCondominio?.ToString());
            queryString.Add("dataInicio", requisicao.DataInicio?.ToString("yyyy-MM-dd"));
            queryString.Add("dataFim", requisicao.DataFim?.ToString("yyyy-MM-dd"));
            queryString.Add("pagina", requisicao.Pagina.ToString());
            queryString.Add("tamanhoPagina", requisicao.TamanhoPagina.ToString());
            queryString.Add("consultarAgendamentosCancelados", requisicao.ConsultarAgendamentosCancelados.ToString());

            var uri = Configuracao.Url.ApiAgendamento.UrlBasePath + Configuracao.Url.ApiAgendamento.ListarEvento + "?" + queryString.ToString();
            
            var resposta = await _httpClient.GetAsync(uri);
            return JsonSerializer.Deserialize<ConsultaPaginada<AgendamentoEventoResult>>(await resposta.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<ProcessamentoBaseResponse> CancelarAgendamento(CancelarEventoRequest requisicao, string usuarioLogado)
        {
            _logger.LogInformation($"[AgendamentoClient] Iniciar requisição HTTP para cancelar um evento");
            var uri = Configuracao.Url.ApiAgendamento.UrlBasePath + Configuracao.Url.ApiAgendamento.CancelarEvento;
            var requisicaoApiAgendamentos = new CancelaAgendamentoEventoRequest(requisicao.IdCondominio, requisicao.IdAreaCondominio, requisicao.DataEvento, usuarioLogado);
            var conteudo = new StringContent(JsonSerializer.Serialize(requisicaoApiAgendamentos), Encoding.UTF8, "application/json");
            var resposta = await _httpClient.PutAsync(uri, conteudo);

            return JsonSerializer.Deserialize<ProcessamentoBaseResponse>(await resposta.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}