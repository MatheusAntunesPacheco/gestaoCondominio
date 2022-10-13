using FluentValidation;
using FluentValidation.Results;
using GestaoAcesso.API.Application.Command.CriptografarTexto;
using GestaoAcesso.API.Entities;
using GestaoAcesso.API.Infrastructure.Interfaces;
using MediatR;

namespace GestaoAcesso.API.Application.Command.CadastrarUsuario
{
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, ValidationResult>
    {
        private readonly ILogger<CadastrarUsuarioCommandHandler> _logger;
        private readonly IValidator<CadastrarUsuarioCommand> _validator;
        private readonly IUsuariosRepository _usuarioRepository;
        private readonly IMediator _mediator;

        public CadastrarUsuarioCommandHandler(ILogger<CadastrarUsuarioCommandHandler> logger, IValidator<CadastrarUsuarioCommand> validator, IUsuariosRepository usuarioRepository, IMediator mediator)
        {
            _logger = logger;
            _validator = validator;
            _usuarioRepository = usuarioRepository;
            _mediator = mediator;
        }
        public async Task<ValidationResult> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[CadastrarUsuarioCommandHandler] Iniciando cadastro do usuário {request.Cpf}");
            var validacao = await _validator.ValidateAsync(request);
            if (!validacao.IsValid)
            {
                _logger.LogWarning($"[CadastrarUsuarioCommandHandler] Requisição para cadastro do usuário {request.Cpf} não está valida: {string.Join(" | ", validacao.Errors.Select(e => e.ErrorMessage))}");
                return validacao;
            }

            if (await UsuarioJaCadastrado(request.Cpf))
            {
                _logger.LogWarning($"[CadastrarUsuarioCommandHandler] Usuário {request.Cpf} ja está cadastrado no banco de dados");
                validacao.Errors.Add(new ValidationFailure("Cpf", "Usuário ja foi cadastrado anteriormente com este Cpf"));
                return validacao;
            }

            var senhaCriptografada = await CriptografarSenha(request.Senha);
            var usuario = await CadastrarUsuarioNoBanco(request, senhaCriptografada);

            return validacao;
        }

        private async Task<bool> UsuarioJaCadastrado(string cpf)
        {
            _logger.LogInformation($"[CadastrarUsuarioCommandHandler] Verificando se usuário {cpf} ja está cadastrado");
            var usuarioCadastrado = await _usuarioRepository.ObterPorCpf(cpf);
            return usuarioCadastrado != null;
        }

        private async Task<string> CriptografarSenha(string senha)
        {
            _logger.LogInformation($"[CadastrarUsuarioCommandHandler] Realizando criptografia de senha");
            var requisicao = new CriptografarTextoCommand(senha);
            return await _mediator.Send(requisicao);
        }

        private async Task<Usuario> CadastrarUsuarioNoBanco(CadastrarUsuarioCommand requisicao, string senhaCriptografada)
        {
            _logger.LogInformation($"[CadastrarUsuarioCommandHandler] Salvando novo usuário {requisicao.Cpf} no banco de dados");

            var usuario = new Usuario(
                requisicao.Cpf,
                requisicao.Nome,
                requisicao.Email,
                senhaCriptografada);

            return await _usuarioRepository.Criar(usuario);
        }
    }
}
