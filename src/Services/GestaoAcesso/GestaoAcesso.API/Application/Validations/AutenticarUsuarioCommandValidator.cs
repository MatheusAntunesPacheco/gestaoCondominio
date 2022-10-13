using FluentValidation;
using GestaoAcesso.API.Application.Command.AutenticarUsuario;

namespace GestaoAcesso.API.Application.Validations
{
    public class AutenticarUsuarioCommandValidator : AbstractValidator<AutenticarUsuarioCommand>
    {
        public AutenticarUsuarioCommandValidator()
        {
            RuleFor(c => c.Senha).NotEmpty().WithMessage("Senha não pode ser vazia");
            RuleFor(c => c.Cpf).NotEmpty().WithMessage("Usuário não pode estar vazio");
        }
    }
}
