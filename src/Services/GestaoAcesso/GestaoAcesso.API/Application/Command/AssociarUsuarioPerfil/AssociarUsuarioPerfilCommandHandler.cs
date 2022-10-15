using GestaoAcesso.API.Application.Command.LerTokenJwt;
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
            var usuario = await _usuarioRepository.ObterPorCpf(request.Cpf);
            if (usuario == null)
            {
                _logger.LogWarning($"[AssociarUsuarioPerfilCommandHandler] Usuário {request.Cpf} não está cadastrado no banco de dados");
                return new ProcessamentoBaseResponse(false, "Usuário não cadastrado no banco de dados");
            }

            var conteudoTokenUsuarioLogado = await _mediator.Send(new LerPayloadTokenJwtCommand(request.TokenJwtUsuarioLogado));
            if (conteudoTokenUsuarioLogado == null)
            {
                _logger.LogWarning("[AssociarUsuarioPerfilCommandHandler] Não foi possível fazer leitura do payload do token JWT");
                return new ProcessamentoBaseResponse(false, "Não foi possível fazer leitura do payload do token do usuário logado");
            }

            if (!VerificarSeUsuarioLogadoPodeEfetuarAssociacao(conteudoTokenUsuarioLogado, request))
            {
                _logger.LogWarning($"[AssociarUsuarioPerfilCommandHandler] Usuario {request.Cpf} não tem permissão para cadastrar o usuário a esse perfil");
                return new ProcessamentoBaseResponse(false, "Usuario não tem permissão para cadastrar o usuário a esse perfil");
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
        /// <param name="payloadToken">Payload Token JWT do usuário logado</param>
        /// <param name="request">Requisição para associação do usuario com um perfil</param>
        /// <returns></returns>
        private bool VerificarSeUsuarioLogadoPodeEfetuarAssociacao(PayloadTokenJwt payloadToken, AssociarUsuarioPerfilCommand request)
        {
            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Verificando se usuário logado {payloadToken.Cpf} pode associar o usuario requisitado {request.Cpf} ao perfil indicado");
            if (request.IdCondominio.HasValue)
                return payloadToken.UsuarioEhAdministradorCondominio(request.IdCondominio.Value);
            else
                return payloadToken.AdministradorGeral;
        }

        private async Task<PerfilUsuario> AtualizarPerfilUsuario(IEnumerable<PerfilUsuario> perfisUsuario, AssociarUsuarioPerfilCommand request)
        {
            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Atualizando perfil do usuário {request.Cpf}");

            if (!request.IdCondominio.HasValue && request.Administrador)
                return await AtualizarPerfilUsuarioAdministradorGeral(request.Cpf, perfisUsuario);

            if (request.IdCondominio.HasValue)
                return await AtualizarPerfilUsuarioCondominio(request, perfisUsuario);

            return new PerfilUsuario(null, request.Cpf, null, false);
        }

        private async Task<PerfilUsuario> AtualizarPerfilUsuarioAdministradorGeral(string cpf, IEnumerable<PerfilUsuario> perfisUsuario)
        {
            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Verificando se {cpf} já é administrador geral");
            var perfilAdm = perfisUsuario.FirstOrDefault(u => !u.IdCondominio.HasValue && u.Administrador);
            if (perfilAdm != null)
            {
                _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Usuario {cpf} já é administrador geral");
                return perfilAdm;
            }

            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Criando perfil de administrador geral para o usuário {cpf}");
            return await _perfilUsuarioRepository.Criar(new PerfilUsuario(null, cpf, null, true));
        }

        private async Task<PerfilUsuario> AtualizarPerfilUsuarioCondominio(AssociarUsuarioPerfilCommand request, IEnumerable<PerfilUsuario> perfisUsuario)
        {
            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Verificando se {request.Cpf} já possui o perfil requisitado no condominio indicado");
            var perfilCondominio = perfisUsuario.FirstOrDefault(u => u.IdCondominio.HasValue && u.IdCondominio.Value == request.IdCondominio.Value);
            if (perfilCondominio != null && perfilCondominio.Administrador != request.Administrador)
            {
                _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Usuario {perfilCondominio.Cpf} já possui perfil associado ao condominio {perfilCondominio.IdCondominio.Value}. Realizando atualização");
                perfilCondominio.AtualizarIndicadorAdministrador(request.Administrador);
                return _perfilUsuarioRepository.Atualizar(perfilCondominio);
            }
            else if(perfilCondominio != null)
            {
                _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Usuario {perfilCondominio.Cpf} já possui perfil associado ao condominio {perfilCondominio.IdCondominio.Value} com a mesma configuração");
                return perfilCondominio;
            }

            _logger.LogInformation($"[AssociarUsuarioPerfilCommandHandler] Criando perfil do usuário {request.Cpf} para o condominip {request.IdCondominio.Value}");
            return await _perfilUsuarioRepository.Criar(new PerfilUsuario(null, request.Cpf, request.IdCondominio, request.Administrador));
        }
    }
}
