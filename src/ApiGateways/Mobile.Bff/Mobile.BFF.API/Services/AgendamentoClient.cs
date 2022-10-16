namespace Mobile.BFF.API.Services
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
    }
}
