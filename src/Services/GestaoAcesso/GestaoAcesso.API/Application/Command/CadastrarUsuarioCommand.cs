using MediatR;

namespace GestaoAcesso.API.Application.Command
{
    public class CadastrarUsuarioCommand : IRequest<bool>
    {
        public string NomeCompleto { get; private set; }
        public string Usuario { get; private set; }
        public string Senha { get; private set; }
        public bool Administrador { get; private set; }
        public bool AutoCadastro { get; private set; }

        public CadastrarUsuarioCommand(
            string nomeCompleto,
            string usuario,
            string senha,
            bool administrador,
            bool autoCadastro)
        {
            NomeCompleto = nomeCompleto;
            Usuario = usuario;
            Senha = senha;
            Administrador = administrador;
            AutoCadastro = autoCadastro;
        }
    }
}
