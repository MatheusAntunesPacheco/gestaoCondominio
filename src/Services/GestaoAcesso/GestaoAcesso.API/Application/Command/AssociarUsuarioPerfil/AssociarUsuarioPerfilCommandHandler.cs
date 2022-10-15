using GestaoAcesso.API.Application.Command.ObterDadosUsuario;
using GestaoAcesso.API.Entities;
using GestaoAcesso.API.Infrastructure.Interfaces;
using MediatR;

namespace GestaoAcesso.API.Application.Command.AssociarUsuarioPerfil
{
    public class AssociarUsuarioPerfilCommandHandler : IRequestHandler<AssociarUsuarioPerfilCommand, ProcessamentoBaseResponse>
    {
        private readonly ILogger<AssociarUsuarioPerfilCommandHandler> _logger;
        private readonly IUsuariosRepository _usuarioRepository;
        private readonly IPerfisUsuariosRepository _perfilUsuarioRepository;
        private readonly IMediator _mediator;

        public AssociarUsuarioPerfilCommandHandler(ILogger<AssociarUsuarioPerfilCommandHandler> logger, IUsuariosRepository usuarioRepository, IPerfisUsuariosRepository perfilUsuarioRepository, IMediator mediator)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _perfilUsuarioRepository = perfilUsuarioRepository;
            _mediator = mediator;
        }
        public async Task<ProcessamentoBaseResponse> Handle(AssociarUsuarioPerfilCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Iniciando associação do usuário {request.Cpf} ao condominio {request.IdCondominio}");

            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Consultando usuário {request.Cpf}");
            
            if (! (await VerificarSeUsuarioLogadoPodeEfetuarAssociacao(request)))
            {
                _logger.LogWarning($"[AssociarUsuarioPerfilCommandHandler] Usuario {request.Cpf} não tem permissão para cadastrar o usuário a esse perfil");
                return new ProcessamentoBaseResponse(false, "Usuario não tem permissão para cadastrar o usuário a esse perfil");
            }

            var usuario = await _usuarioRepository.ObterPorCpf(request.Cpf);
            if (usuario == null)
            {
                _logger.LogWarning($"[AssociarUsuarioPerfilCommandHandler] Usuário {request.Cpf} não está cadastrado no banco de dados");
                return new ProcessamentoBaseResponse(false, "Usuário não cadastrado no banco de dados");
            }

            var perfisUsuario = _perfilUsuarioRepository.ListarPorCpf(request.Cpf);
            await AtualizarPerfilUsuario(perfisUsuario, request);


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
        private async Task<bool> VerificarSeUsuarioLogadoPodeEfetuarAssociacao(AssociarUsuarioPerfilCommand request)
        {
            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Consultando perfis do usuário logado {request.CpfUsuarioLogado}");
            var usuarioLogado = await _mediator.Send(new ObterDadosUsuarioCommand(request.CpfUsuarioLogado));

            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Verificando se usuário logado {usuarioLogado.Cpf} pode associar o usuario requisitado {request.Cpf} ao perfil indicado");
            if (request.IdCondominio.HasValue)
                return usuarioLogado.UsuarioAdministradorCondominio(request.IdCondominio.Value);
            else
                return usuarioLogado.UsuarioAdministradorGeral;
        }

        private async Task<PerfilUsuario> AtualizarPerfilUsuario(IEnumerable<PerfilUsuario> perfisUsuario, AssociarUsuarioPerfilCommand request)
        {
            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Atualizando perfil do usuário {request.Cpf}");

            if (!request.IdCondominio.HasValue && request.Administrador)
                return await AtualizarPerfilUsuarioAdministradorGeral(request, perfisUsuario);

            if (request.IdCondominio.HasValue)
                return await AtualizarPerfilUsuarioCondominio(request, perfisUsuario);

            return new PerfilUsuario(null, request.Cpf, null, false, request.CpfUsuarioLogado, DateTime.UtcNow);
        }

        private async Task<PerfilUsuario> AtualizarPerfilUsuarioAdministradorGeral(AssociarUsuarioPerfilCommand request, IEnumerable<PerfilUsuario> perfisUsuario)
        {
            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Verificando se {request.Cpf} já é administrador geral");
            var perfilAdm = perfisUsuario.FirstOrDefault(u => !u.IdCondominio.HasValue && u.Administrador);
            if (perfilAdm != null)
            {
                _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Usuario {request.Cpf} já é administrador geral");
                return perfilAdm;
            }

            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Criando perfil de administrador geral para o usuário {request.Cpf}");
            return await _perfilUsuarioRepository.Criar(new PerfilUsuario(null, request.Cpf, null, true, request.CpfUsuarioLogado, DateTime.UtcNow));
        }

        private async Task<PerfilUsuario> AtualizarPerfilUsuarioCondominio(AssociarUsuarioPerfilCommand request, IEnumerable<PerfilUsuario> perfisUsuario)
        {
            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Verificando se {request.Cpf} já possui o perfil requisitado no condominio indicado");
            var perfilCondominio = perfisUsuario.FirstOrDefault(u => u.IdCondominio.HasValue && u.IdCondominio.Value == request.IdCondominio.Value);
            if (perfilCondominio != null && perfilCondominio.Administrador != request.Administrador)
            {
                _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Usuario {perfilCondominio.Cpf} já possui perfil associado ao condominio {perfilCondominio.IdCondominio.Value}. Realizando atualização");
                perfilCondominio.AtualizarIndicadorAdministrador(request.Administrador, request.CpfUsuarioLogado);
                return _perfilUsuarioRepository.Atualizar(perfilCondominio);
            }
            else if(perfilCondominio != null)
            {
                _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Usuario {perfilCondominio.Cpf} já possui perfil associado ao condominio {perfilCondominio.IdCondominio.Value} com a mesma configuração");
                return perfilCondominio;
            }

            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Criando perfil do usuário {request.Cpf} para o condominip {request.IdCondominio.Value}");
            return await _perfilUsuarioRepository.Criar(new PerfilUsuario(null, request.Cpf, request.IdCondominio, request.Administrador, request.CpfUsuarioLogado, DateTime.UtcNow));
        }
    }
}
