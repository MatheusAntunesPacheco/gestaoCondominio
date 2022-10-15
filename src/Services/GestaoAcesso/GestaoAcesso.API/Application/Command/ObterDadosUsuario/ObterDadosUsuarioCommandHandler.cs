using GestaoAcesso.API.Domain;
using GestaoAcesso.API.Infrastructure.Interfaces;
using MediatR;

namespace GestaoAcesso.API.Application.Command.ObterDadosUsuario
{
    public class ObterDadosUsuarioCommandHandler : IRequestHandler<ObterDadosUsuarioCommand, Usuario>
    {
        private readonly ILogger<ObterDadosUsuarioCommandHandler> _logger;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IPerfisUsuariosRepository _perfisUsuariosRepository;

        public ObterDadosUsuarioCommandHandler(ILogger<ObterDadosUsuarioCommandHandler> logger, IUsuariosRepository usuarioRepository, 
            IPerfisUsuariosRepository perfisUsuariosRepository)
        {
            _logger = logger;
            _usuariosRepository = usuarioRepository;
            _perfisUsuariosRepository = perfisUsuariosRepository;
        }
        public async Task<Usuario> Handle(ObterDadosUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[ObterDadosUsuarioCommandHandler] Iniciando consulta por dados completos do usuário {request.Cpf}");

            _logger.LogInformation($"[ObterDadosUsuarioCommandHandler] Consultando usuário {request.Cpf}");
            var usuario = await _usuariosRepository.ObterPorCpf(request.Cpf);
            if (usuario == null)
            {
                _logger.LogWarning($"[ObterDadosUsuarioCommandHandler] Usuário {request.Cpf} não está cadastrado no banco de dados");
                return null;
            }

            _logger.LogInformation($"[ObterDadosUsuarioCommandHandler] Obtendo perfis do usuario {request.Cpf}");
            var perfisUsuario = _perfisUsuariosRepository.ListarPorCpf(request.Cpf);

            _logger.LogInformation($"[ObterDadosUsuarioCommandHandler] Montando objeto Usuario do {request.Cpf}");
            return new Usuario(usuario.Cpf, perfisUsuario.Select(p => new Perfil(p.IdCondominio, p.Administrador)));
        }
    }
}
