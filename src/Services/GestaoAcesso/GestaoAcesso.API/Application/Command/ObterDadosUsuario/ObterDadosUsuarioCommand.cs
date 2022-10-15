using GestaoAcesso.API.Domain;
using MediatR;

namespace GestaoAcesso.API.Application.Command.ObterDadosUsuario
{
    /// <summary>
    /// Requisição utilizada para criar novo usuario no sistema
    /// </summary>
    public class ObterDadosUsuarioCommand : IRequest<Usuario>
    {

        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string Cpf { get; private set; }

        public ObterDadosUsuarioCommand(string cpf)
        {
            Cpf = cpf;
        }
    }
}