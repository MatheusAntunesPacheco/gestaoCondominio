using GestaoAcesso.API.Entities;
using MediatR;

namespace GestaoAcesso.API.Application.Command.GerarTokenJwt
{
    /// <summary>
    /// Requisição utilizada para gerar token JWT para autenticação do usuário
    /// </summary>
    public class GerarTokenJwtCommand : IRequest<GerarTokenJwtResponse>
    {
        /// <summary>
        /// Usuário autenticado
        /// </summary>
        public Usuario UsuarioAutenticado { get; private set; }

        /// <summary>
        /// Lista de perfis do usuario autenticado
        /// </summary>
        public IEnumerable<PerfilUsuario> PerfisUsuarioAutenticado { get; private set; }

        public GerarTokenJwtCommand(Usuario usuarioAutenticado, IEnumerable<PerfilUsuario> perfisUsuarioAutenticado)
        {
            UsuarioAutenticado = usuarioAutenticado;
            PerfisUsuarioAutenticado = perfisUsuarioAutenticado;
        }
    }
}