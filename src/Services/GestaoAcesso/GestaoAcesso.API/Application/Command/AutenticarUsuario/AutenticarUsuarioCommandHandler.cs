using FluentValidation;
using GestaoAcesso.API.Application.Command.CriptografarTexto;
using GestaoAcesso.API.Application.Command.GerarTokenJwt;
using GestaoAcesso.API.Infrastructure.Interfaces;
using MediatR;

namespace GestaoAcesso.API.Application.Command.AutenticarUsuario
{
    public class AutenticarUsuarioCommandHandler : IRequestHandler<AutenticarUsuarioCommand, AutenticarUsuarioResponse>
    {
        private readonly ILogger<AutenticarUsuarioCommandHandler> _logger;
        private readonly IValidator<AutenticarUsuarioCommand> _validator;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IPerfisUsuariosRepository _perfisUsuariosRepository;
        private readonly IMediator _mediator;

        public AutenticarUsuarioCommandHandler(ILogger<AutenticarUsuarioCommandHandler> logger, IValidator<AutenticarUsuarioCommand> validator, IUsuariosRepository usuarioRepository, 
            IPerfisUsuariosRepository perfisUsuariosRepository, IMediator mediator)
        {
            _logger = logger;
            _validator = validator;
            _usuariosRepository = usuarioRepository;
            _perfisUsuariosRepository = perfisUsuariosRepository;
            _mediator = mediator;
        }
        public async Task<AutenticarUsuarioResponse> Handle(AutenticarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[AutenticarUsuarioCommandHandler] Iniciando autenticação do usuário {request.Cpf}");
            var validacao = await _validator.ValidateAsync(request);
            if (!validacao.IsValid)
            {
                _logger.LogWarning($"[AutenticarUsuarioCommandHandler] Requisição para autenticação do usuário {request.Cpf} não está valida: {string.Join(" | ", validacao.Errors.Select(e => e.ErrorMessage))}");
                return new AutenticarUsuarioResponse(false);
            }

            _logger.LogInformation($"[AutenticarUsuarioCommandHandler] Consultando usuário {request.Cpf}");
            var usuario = await _usuariosRepository.ObterPorCpf(request.Cpf);
            if (usuario == null)
            {
                _logger.LogWarning($"[AutenticarUsuarioCommandHandler] Usuário {request.Cpf} não está cadastrado no banco de dados");
                return new AutenticarUsuarioResponse(false);
            }

            _logger.LogInformation($"[AutenticarUsuarioCommandHandler] Validando senha do usuário {request.Cpf}");
            var senhaCriptografada = await CriptografarSenha(request.Senha);

            if (senhaCriptografada != usuario.SenhaCriptografada)
            {
                _logger.LogWarning($"[AutenticarUsuarioCommandHandler] Senha preenchida para o Usuário {request.Cpf} não confere");
                return new AutenticarUsuarioResponse(false);
            }

            _logger.LogInformation($"[AutenticarUsuarioCommandHandler] Usuário {request.Cpf} autenticado com sucesso. Obtendo perfis do usuario");
            var perfisUsuario = _perfisUsuariosRepository.ListarPorCpf(request.Cpf);

            _logger.LogInformation($"[AutenticarUsuarioCommandHandler] Gerando token JWT do usuário {request.Cpf}");
            var tokenJwt = await _mediator.Send(new GerarTokenJwtCommand(usuario, perfisUsuario));

            return new AutenticarUsuarioResponse(true, tokenJwt.Token, tokenJwt.DataCriacaoToken, tokenJwt.DataCriacaoToken);
        }

        private async Task<string> CriptografarSenha(string senha)
        {
            _logger.LogInformation($"[CadastrarUsuarioCommandHandler] Realizando criptografia de senha");
            var requisicao = new CriptografarTextoCommand(senha);
            return await _mediator.Send(requisicao);
        }
    }
}
