using MediatR;

namespace GestaoAcesso.API.Application.Command
{
    public class CadastrarUsuarioCommand : IRequest<bool>
    {
        public string NomeCompleto { get; private set; }
        public string Usuario { get; private set; }
        public string Senha { get; private set; }

        public CadastrarUsuarioCommand(
            string nomeCompleto,
            string usuario,
            string senha)
        {
            NomeCompleto = nomeCompleto;
            Usuario = usuario;
            Senha = senha;
        }
    }
}
