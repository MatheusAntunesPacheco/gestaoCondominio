using FluentValidation;
using GestaoAcesso.API.Application.Command;

namespace GestaoAcesso.API.Application.Validations
{
    public class CadastrarUsuarioCommandValidator: AbstractValidator<CadastrarUsuarioCommand>
    {
        public CadastrarUsuarioCommandValidator()
        {
            RuleFor(c => c.Senha).NotEmpty().WithMessage("Senha não pode ser vazia");
            RuleFor(c => c.NomeCompleto).NotEmpty().WithMessage("Nome completo não pode estar vazio");
            RuleFor(c => c.Usuario).NotEmpty().WithMessage("Usuário não pode estar vazio");
        }
    }
}
