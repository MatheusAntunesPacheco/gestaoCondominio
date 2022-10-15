using GestaoAcesso.API.Entities;
using MediatR;
using System.Security.Cryptography;

namespace GestaoAcesso.API.Application.Command.LerTokenJwt
{
    public class LerPayloadTokenJwtCommandHandler : IRequestHandler<LerPayloadTokenJwtCommand, PayloadTokenJwt>
    {
        private readonly ILogger<LerPayloadTokenJwtCommandHandler> _logger;
        private readonly HashAlgorithm _hashAlgorithm;
        public LerPayloadTokenJwtCommandHandler(ILogger<LerPayloadTokenJwtCommandHandler> logger)
        {
            _logger = logger;
            _hashAlgorithm = SHA512.Create();
        }

        public async Task<PayloadTokenJwt> Handle(LerPayloadTokenJwtCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[LerPayloadTokenJwtCommandHandler] Iniciando leitura do payload do token JWT");

            return new PayloadTokenJwt("a", "b", false, null, null);
        }
    }
}
