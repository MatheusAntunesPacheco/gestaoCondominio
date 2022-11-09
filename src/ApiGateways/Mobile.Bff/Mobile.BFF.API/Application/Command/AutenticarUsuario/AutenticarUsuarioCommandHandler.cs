using MediatR;
using Mobile.BFF.API.Application.Command.GerarTokenJwt;
using Mobile.BFF.API.Models.GestaoAcesso;
using Mobile.BFF.API.Services.GestaoAcessos;

namespace Mobile.BFF.API.Application.Command.AutenticarUsuario
{
    public class AutenticarUsuarioCommandHandler : IRequestHandler<AutenticarUsuarioCommand, AutenticarUsuarioResponse>
    {
        private readonly ILogger<AutenticarUsuarioCommandHandler> _logger;
        private readonly IGestaoAcessoClient _gestaoAcessoClient;
        private readonly IMediator _mediator;

        public AutenticarUsuarioCommandHandler(ILogger<AutenticarUsuarioCommandHandler> logger, IGestaoAcessoClient gestaoAcessoClient, IMediator mediator)
        {
            _logger = logger;
            _gestaoAcessoClient = gestaoAcessoClient;
            _mediator = mediator;
        }
        public async Task<AutenticarUsuarioResponse> Handle(AutenticarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[AutenticarUsuarioCommandHandler] Iniciando autenticação do usuário {request.Cpf}");

            _logger.LogInformation($"[AutenticarUsuarioCommandHandler] Autenticando usuário {request.Cpf} na API Gestão de Acessos");
            var resultadoAutenticacao = await _gestaoAcessoClient.AutenticarUsuario(new AutenticacaoUsuarioRequest(request.Cpf, request.Senha));
            if (resultadoAutenticacao == null || !resultadoAutenticacao.Autenticado)
            {
                _logger.LogWarning($"[AutenticarUsuarioCommandHandler] Não foi possível autenticar o usuário {request.Cpf}");
                return new AutenticarUsuarioResponse(false);
            }

            _logger.LogInformation($"[AutenticarUsuarioCommandHandler] Gerando token JWT do usuário {request.Cpf}");
            var tokenJwt = await _mediator.Send(new GerarTokenJwtCommand(
                    resultadoAutenticacao.Cpf,
                    resultadoAutenticacao.Nome,
                    resultadoAutenticacao.AdministradorGeral,
                    resultadoAutenticacao.CondominiosAdministrador,
                    resultadoAutenticacao.CondominiosUsuarioComum
                ));

            return new AutenticarUsuarioResponse(true, tokenJwt.Token, tokenJwt.DataCriacaoToken, tokenJwt.DataCriacaoToken);
        }
    }
}
