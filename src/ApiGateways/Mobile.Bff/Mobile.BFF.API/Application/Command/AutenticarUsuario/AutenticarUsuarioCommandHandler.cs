using MediatR;
using Mobile.BFF.API.Application.Command.GerarTokenJwt;
using Mobile.BFF.API.Models;
using Mobile.BFF.API.Services;

namespace Mobile.BFF.API.Application.Command.AutenticarUsuario
{
    public class AutenticarUsuarioCommandHandler : IRequestHandler<AutenticarUsuarioCommand, AutenticarUsuarioResponse>
    {
        private readonly ILogger<AutenticarUsuarioCommandHandler> _logger;
        private readonly IGestaoAcessoService _gestaoAcessoService;
        private readonly IMediator _mediator;

        public AutenticarUsuarioCommandHandler(ILogger<AutenticarUsuarioCommandHandler> logger, IGestaoAcessoService gestaoAcessoService, IMediator mediator)
        {
            _logger = logger;
            _gestaoAcessoService = gestaoAcessoService;
            _mediator = mediator;
        }
        public async Task<AutenticarUsuarioResponse> Handle(AutenticarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[AutenticarUsuarioCommandHandler] Iniciando autenticação do usuário {request.Cpf}");

            _logger.LogInformation($"[AutenticarUsuarioCommandHandler] Autenticando usuário {request.Cpf} na API Gestão de Acessos");
            var resultadoAutenticacao = await _gestaoAcessoService.AutenticarUsuario(new AutenticacaoUsuarioRequest(request.Cpf, request.Senha));
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
