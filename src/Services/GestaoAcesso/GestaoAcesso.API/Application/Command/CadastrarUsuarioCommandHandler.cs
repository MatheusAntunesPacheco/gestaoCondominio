using FluentValidation;
using MediatR;

namespace GestaoAcesso.API.Application.Command
{
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, bool>
    {
        private readonly IValidator<CadastrarUsuarioCommand> _validator;
        public CadastrarUsuarioCommandHandler(IValidator<CadastrarUsuarioCommand> validator)
        {
            _validator = validator;
            // TODO construir repositorio
        }
        public async Task<bool> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            // TODO verificar se usuário ja não está cadastrado
            // TODO criptografar senha
            // TODO salvar usuário no banco
            // TODO Notificar admin sobre necessidade de aprovação de cadastro do usuário

            return true;
        }
    }
}
