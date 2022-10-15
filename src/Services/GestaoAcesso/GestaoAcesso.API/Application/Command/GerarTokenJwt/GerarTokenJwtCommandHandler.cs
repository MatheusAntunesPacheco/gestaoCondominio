using GestaoAcesso.API.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GestaoAcesso.API.Application.Command.GerarTokenJwt
{
    /// <summary>
    /// Geração de token JWT para autenticação do usuário
    /// </summary>
    public class GerarTokenJwtCommandHandler : IRequestHandler<GerarTokenJwtCommand, GerarTokenJwtResponse>
    {
        private readonly ILogger<GerarTokenJwtCommandHandler> _logger;

        public GerarTokenJwtCommandHandler(ILogger<GerarTokenJwtCommandHandler> logger)
        {
            _logger = logger;
        }

        public async Task<GerarTokenJwtResponse> Handle(GerarTokenJwtCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[GerarTokenJwtCommandHandler] Iniciando geração de token JWT");
            var dataCriacao = DateTime.Now;
            var dataExpiracao = dataCriacao + Configuracao.Jwt.TempoExpiracaoToken;

            var payload = new PayloadTokenJwt(
                request.UsuarioAutenticado.Cpf, 
                request.UsuarioAutenticado.Nome, 
                VerificarSeUsuarioEhAdministradorGeral(request.PerfisUsuarioAutenticado),
                request.PerfisUsuarioAutenticado.Where(u => u.Administrador && u.IdCondominio.HasValue).Select(u => u.IdCondominio.Value),
                request.PerfisUsuarioAutenticado.Where(u => !u.Administrador && u.IdCondominio.HasValue).Select(u => u.IdCondominio.Value)
            );

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
                    { "DadosUsuario", payload }
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

        private bool VerificarSeUsuarioEhAdministradorGeral(IEnumerable<PerfilUsuario> listaPerfisUsuario) => listaPerfisUsuario.Any(u => !u.IdCondominio.HasValue && u.Administrador);
    }
}
