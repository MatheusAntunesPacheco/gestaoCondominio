using GestaoAcesso.API.Entities;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace GestaoAcesso.API.Application.Command.LerTokenJwt
{
    public class LerPayloadTokenJwtCommandHandler : IRequestHandler<LerPayloadTokenJwtCommand, PayloadTokenJwt>
    {
        private readonly ILogger<LerPayloadTokenJwtCommandHandler> _logger;
        public LerPayloadTokenJwtCommandHandler(ILogger<LerPayloadTokenJwtCommandHandler> logger)
        {
            _logger = logger;
        }

        public async Task<PayloadTokenJwt> Handle(LerPayloadTokenJwtCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[LerPayloadTokenJwtCommandHandler] Iniciando leitura do payload do token JWT");
            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadJwtToken(request.TokenJwt);
            var payloaDadosUsuario = token.Claims.FirstOrDefault(p => p.Type == "DadosUsuario").Value;
            if (string.IsNullOrWhiteSpace(payloaDadosUsuario))
            {
                _logger.LogInformation("[LerPayloadTokenJwtCommandHandler] Token JWT não possui dados do usuário no formato correto");
                return null;
            }

            var dadosUsuarios = JsonSerializer.Deserialize<PayloadTokenJwt>(payloaDadosUsuario);
            return dadosUsuarios;
        }
    }
}
