using MediatR;

namespace GestaoAcesso.API.Application.Command.CadastrarUsuario
{
    /// <summary>
    /// Requisição utilizada para criar novo usuario no sistema
    /// </summary>
    public class CadastrarUsuarioCommand : IRequest<ProcessamentoBaseResponse>
    {
        /// <summary>
        /// Nome do usuario
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Senha do usuario
        /// </summary>
        public string Senha { get; private set; }

        /// <summary>
        /// E-mail do usuario
        /// </summary>
        public string Email { get; private set; }

        public CadastrarUsuarioCommand(
            string nome,
            string cpf,
            string senha,
            string email)
        {
            Nome = nome;
            Cpf = cpf;
            Senha = senha;
            Email = email;
        }
    }
}