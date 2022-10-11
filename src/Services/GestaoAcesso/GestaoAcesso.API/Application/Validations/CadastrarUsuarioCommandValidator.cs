using FluentValidation;
using GestaoAcesso.API.Application.Command.CadastrarUsuario;

namespace GestaoAcesso.API.Application.Validations
{
    public class CadastrarUsuarioCommandValidator: AbstractValidator<CadastrarUsuarioCommand>
    {
        public CadastrarUsuarioCommandValidator()
        {
            RuleFor(c => c.Senha).NotEmpty().WithMessage("Senha não pode ser vazia");
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Nome completo não pode estar vazio");
            RuleFor(c => c.Cpf).NotEmpty().WithMessage("Usuário não pode estar vazio");
        }
    }
}
