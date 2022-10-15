using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace GestaoAcesso.API.Application.Command.GerarTokenJwt
{
    public class GerarTokenJwtCommandHandler : IRequestHandler<GerarTokenJwtCommand, GerarTokenJwtResponse>
    {
        private readonly ILogger<GerarTokenJwtCommandHandler> _logger;
        private readonly HashAlgorithm _hashAlgorithm;
        public GerarTokenJwtCommandHandler(ILogger<GerarTokenJwtCommandHandler> logger)
        {
            _logger = logger;
            _hashAlgorithm = SHA512.Create();
        }

        public async Task<GerarTokenJwtResponse> Handle(GerarTokenJwtCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[GerarTokenJwtCommandHandler] Iniciando geração de token JWT");
            var dataCriacao = DateTime.Now;
            var dataExpiracao = dataCriacao + Configuracao.Jwt.TempoExpiracaoToken;

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Configuracao.Jwt.Issuer,
                Audience = Configuracao.Jwt.Audience,
                SigningCredentials = CriarCredenciaisJwt(),
                NotBefore = dataCriacao,
                Expires = dataExpiracao,
                Claims = new Dictionary<string, object>()
                {
                    { "NomeUsuario", request.NomeUsuario },
                    { "CpfUsuario", request.CpfUsuario }
                }
            });
            var token = handler.WriteToken(securityToken);

            return new GerarTokenJwtResponse(token, dataCriacao, dataExpiracao);
        }

        private SigningCredentials CriarCredenciaisJwt()
        {
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuracao.Jwt.ChaveSecreta));
            return new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
        }
    }
}
