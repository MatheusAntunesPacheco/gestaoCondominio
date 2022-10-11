using MediatR;

namespace GestaoAcesso.API.Application.Command.CriptografarTexto
{
    /// <summary>
    /// Requisição utilizada para criptografar texto utilizado na api para senha de usuario
    /// </summary>
    public class CriptografarTextoCommand : IRequest<string>
    {
        /// <summary>
        /// Texto a ser criptografado
        /// </summary>
        public string Texto { get; private set; }

        public CriptografarTextoCommand(string texto)
        {
            Texto = texto;
        }
    }
}