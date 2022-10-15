using MediatR;

namespace GestaoAcesso.API.Application.Command.GerarTokenJwt
{
    /// <summary>
    /// Requisição utilizada para criptografar texto utilizado na api para senha de usuario
    /// </summary>
    public class GerarTokenJwtCommand : IRequest<GerarTokenJwtResponse>
    {
        /// <summary>
        /// Nome do usuário autenticado
        /// </summary>
        public string NomeUsuario { get; private set; }

        /// <summary>
        /// CPF do usuário autenticado
        /// </summary>
        public string CpfUsuario { get; private set; }

        public GerarTokenJwtCommand(string nomeUsuario, string cpfUsuario)
        {
            NomeUsuario = nomeUsuario;
            CpfUsuario = cpfUsuario;
        }
    }
}