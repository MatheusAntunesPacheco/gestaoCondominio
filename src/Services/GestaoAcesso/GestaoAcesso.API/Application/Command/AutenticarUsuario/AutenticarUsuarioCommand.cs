using FluentValidation.Results;
using MediatR;

namespace GestaoAcesso.API.Application.Command.AutenticarUsuario
{
    /// <summary>
    /// Requisição utilizada para criar novo usuario no sistema
    /// </summary>
    public class AutenticarUsuarioCommand : IRequest<AutenticarUsuarioResponse>
    {

        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Senha do usuario
        /// </summary>
        public string Senha { get; private set; }

        public AutenticarUsuarioCommand(string cpf, string senha)
        {
            Cpf = cpf;
            Senha = senha;
        }
    }
}