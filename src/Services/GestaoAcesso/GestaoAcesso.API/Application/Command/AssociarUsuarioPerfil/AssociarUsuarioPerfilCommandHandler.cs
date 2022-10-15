using GestaoAcesso.API.Application.Command.CriptografarTexto;
using GestaoAcesso.API.Application.Command.LerTokenJwt;
using GestaoAcesso.API.Infrastructure.Interfaces;
using MediatR;

namespace GestaoAcesso.API.Application.Command.AssociarUsuarioPerfil
{
    public class AssociarUsuarioPerfilCommandHandler : IRequestHandler<AssociarUsuarioPerfilCommand, ProcessamentoBaseResponse>
    {
        private readonly ILogger<AssociarUsuarioPerfilCommandHandler> _logger;
        private readonly IUsuariosRepository _usuarioRepository;
        private readonly IMediator _mediator;

        public AssociarUsuarioPerfilCommandHandler(ILogger<AssociarUsuarioPerfilCommandHandler> logger, IUsuariosRepository usuarioRepository, IMediator mediator)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
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
                _logger.LogWarning("Não foi possível fazer leitura do payload do token JWT");
                return new ProcessamentoBaseResponse(false, "Não foi possível fazer leitura do payload do token do usuário logado");
            }

            // TODO verificar se usuário que solicitou a associação é adm do condomínio cujo novo usuário será associado

            // TODO verifcar se usuário já possui perfil (Se possuir, atualizar, senão criar)


            return new ProcessamentoBaseResponse(true, string.Empty);
        }

        private async Task<string> CriptografarSenha(string senha)
        {
            _logger.LogInformation($"[CadastrarUsuarioCommandHandler] Realizando criptografia de senha");
            var requisicao = new CriptografarTextoCommand(senha);
            return await _mediator.Send(requisicao);
        }
    }
}
