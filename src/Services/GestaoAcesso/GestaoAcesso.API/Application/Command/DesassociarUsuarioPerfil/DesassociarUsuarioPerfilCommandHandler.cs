using GestaoAcesso.API.Application.Command.ObterDadosUsuario;
using GestaoAcesso.API.Entities;
using GestaoAcesso.API.Infrastructure.Interfaces;
using MediatR;

namespace GestaoAcesso.API.Application.Command.DesassociarUsuarioPerfil
{
    public class DesassociarUsuarioPerfilCommandHandler : IRequestHandler<DesassociarUsuarioPerfilCommand, ProcessamentoBaseResponse>
    {
        private readonly ILogger<DesassociarUsuarioPerfilCommandHandler> _logger;
        private readonly IUsuariosRepository _usuarioRepository;
        private readonly IPerfisUsuariosRepository _perfilUsuarioRepository;
        private readonly IMediator _mediator;

        public DesassociarUsuarioPerfilCommandHandler(ILogger<DesassociarUsuarioPerfilCommandHandler> logger, IUsuariosRepository usuarioRepository, IPerfisUsuariosRepository perfilUsuarioRepository, IMediator mediator)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _perfilUsuarioRepository = perfilUsuarioRepository;
            _mediator = mediator;
        }
        public async Task<ProcessamentoBaseResponse> Handle(DesassociarUsuarioPerfilCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[DesassociarUsuarioPerfilCommandHandler] Iniciando desassociação do usuário {request.Cpf} ao condominio {request.IdCondominio}");

            _logger.LogInformation($"[DesassociarUsuarioPerfilCommandHandler] Consultando usuário {request.Cpf}");
            var usuario = await _usuarioRepository.ObterPorCpf(request.Cpf);
            if (usuario == null)
            {
                _logger.LogWarning($"[DesassociarUsuarioPerfilCommandHandler] Usuário {request.Cpf} não está cadastrado no banco de dados");
                return new ProcessamentoBaseResponse(false, "Usuário não cadastrado no banco de dados");
            }

            if (! (await VerificarSeUsuarioLogadoPodeEfetuarAssociacao(request)))
            {
                _logger.LogWarning($"[DesassociarUsuarioPerfilCommandHandler] Usuario {request.Cpf} não tem permissão para cadastrar o usuário a esse perfil");
                return new ProcessamentoBaseResponse(false, "Usuario não tem permissão para cadastrar o usuário a esse perfil");
            }

            var perfisUsuario = _perfilUsuarioRepository.ListarPorCpf(request.Cpf);
            RemoverPerfilUsuario(perfisUsuario, request);
            return new ProcessamentoBaseResponse(true, string.Empty);
        }

        /// <summary>
        /// Regras
        ///     Só poderá associar um usuário ao perfil (seja ele morador comum ou admin) aquele usuário que for administrador do condominio
        ///     Só poderá associar um usuário ao perfil de administrador geral aquele usuario que for administrador geral
        /// </summary>
        /// <param name="usuarioLogado">Usuário logado</param>
        /// <param name="request">Requisição para associação do usuario com um perfil</param>
        /// <returns></returns>
        private async Task<bool> VerificarSeUsuarioLogadoPodeEfetuarAssociacao(DesassociarUsuarioPerfilCommand request)
        {
            _logger.LogInformation($"[DesassociarUsuarioPerfilCommandHandler] Consultando perfis do usuário logado {request.CpfUsuarioLogado}");
            var usuarioLogado = await _mediator.Send(new ObterDadosUsuarioCommand(request.CpfUsuarioLogado));
            if (usuarioLogado == null)
                return false;

            _logger.LogInformation($"[DesassociarUsuarioPerfilCommandHandler] Verificando se usuário logado {usuarioLogado.Cpf} pode desassociar o usuario requisitado {request.Cpf} ao perfil indicado");
            if (request.IdCondominio.HasValue)
                return usuarioLogado.UsuarioAdministradorCondominio(request.IdCondominio.Value);
            else
                return usuarioLogado.UsuarioAdministradorGeral;
        }

        private void RemoverPerfilUsuario(IEnumerable<PerfilUsuario> perfisUsuario, DesassociarUsuarioPerfilCommand request)
        {
            _logger.LogInformation($"[DesassociarUsuarioPerfilCommandHandler] Removendo perfil do usuário {request.Cpf}");

            PerfilUsuario perfilARemover = null;
            if (request.IdCondominio.HasValue)
                perfilARemover = perfisUsuario.FirstOrDefault(p => p.IdCondominio.HasValue && p.IdCondominio.Value == request.IdCondominio.Value);
            else
                perfilARemover = perfisUsuario.FirstOrDefault(p => !p.IdCondominio.HasValue);

            if (perfilARemover == null)
                return;

            _perfilUsuarioRepository.Remover(perfilARemover);
        }
    }
}
