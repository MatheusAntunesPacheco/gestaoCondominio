using MediatR;

namespace GestaoAcesso.API.Application.Command
{
    /// <summary>
    /// Requisição utilizada para criar novo usuario no sistema
    /// </summary>
    public class CadastrarUsuarioCommand : IRequest<bool>
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
        /// Indicador que define se usuário é administrador ou usuario comum
        /// </summary>
        public bool Administrador { get; private set; }

        /// <summary>
        /// Indicador que define se usuário precisa de aprovação do cadastro por parte de um administrador
        /// </summary>
        public bool NecessitaAprovacaoCadastro { get; private set; }

        public CadastrarUsuarioCommand( 
            string nome,
            string cpf,
            string senha,
            bool administrador,
            bool necessitaAprovacaoCadastro)
         {
            Nome = nome;
            Cpf = cpf;
            Senha = senha;
            Administrador = administrador;
            NecessitaAprovacaoCadastro = necessitaAprovacaoCadastro;
        }
    }
}