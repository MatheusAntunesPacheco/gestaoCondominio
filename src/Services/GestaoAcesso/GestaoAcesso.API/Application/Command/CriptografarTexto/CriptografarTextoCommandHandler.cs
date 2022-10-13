using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace GestaoAcesso.API.Application.Command.CriptografarTexto
{
    public class CriptografarTextoCommandHandler : IRequestHandler<CriptografarTextoCommand, string>
    {
        private readonly ILogger<CriptografarTextoCommandHandler> _logger;
        private readonly HashAlgorithm _hashAlgorithm;
        public CriptografarTextoCommandHandler(ILogger<CriptografarTextoCommandHandler> logger)
        {
            _logger = logger;
            _hashAlgorithm = SHA512.Create();
        }

        public async Task<string> Handle(CriptografarTextoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[CriptografarSenhaCommandHandler] Iniciando criptografia");
            if (string.IsNullOrEmpty(request.Texto))
            {
                _logger.LogWarning("[CriptografarSenhaCommandHandler] Texto vazio. Não foi possível executar a criptografia");
                return string.Empty;
            }

            var valorBytes = Encoding.UTF8.GetBytes(request.Texto);
            var arrayBytesCriptografado = _hashAlgorithm.ComputeHash(valorBytes);

            var textoCriptografado = new StringBuilder();
            foreach (var caracter in arrayBytesCriptografado)
            {
                textoCriptografado.Append(caracter.ToString("X2"));
            }

            return textoCriptografado.ToString();
        }
    }
}
