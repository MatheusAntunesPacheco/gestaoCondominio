namespace GestaoAcesso.API.Application.Command.AutenticarUsuario
{
    /// <summary>
    /// Requisição utilizada para criptografar texto utilizado na api para senha de usuario
    /// </summary>
    public class AutenticarUsuarioResponse
    {
        /// <summary>
        /// Atributo que identifica se usuário foi autenticado
        /// </summary>
        public bool Autenticado { get; private set; }

        /// <summary>
        /// CPF dp usuário
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Atributo que indica se o usuário é administrador do sistema
        /// </summary>
        public bool AdministradorGeral { get; private set; }

        /// <summary>
        /// Lista de Id's de condomínio cujo usuário autenticado é o administrador
        /// </summary>
        public IEnumerable<int> CondominiosAdministrador { get; private set; }

        /// <summary>
        /// Lista de Id's de condomínio cujo usuário autenticado é usuário comum
        /// </summary>
        public IEnumerable<int> CondominiosUsuarioComum { get; private set; }

        public AutenticarUsuarioResponse(bool autenticado, string cpf, string nome, bool administradorGeral, IEnumerable<int> condominiosAdministrador, IEnumerable<int> condominiosUsuarioComum)
        {
            Autenticado = autenticado;
            Cpf = cpf;
            Nome = nome;
            AdministradorGeral = administradorGeral;
            CondominiosAdministrador = condominiosAdministrador;
            CondominiosUsuarioComum = condominiosUsuarioComum;
        }

        public AutenticarUsuarioResponse(bool autenticado)
        {
            Autenticado = autenticado;
        }
    }
}