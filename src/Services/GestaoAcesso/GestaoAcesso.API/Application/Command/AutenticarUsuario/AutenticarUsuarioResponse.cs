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
        /// Token JWT gerado
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Data de criação do Token
        /// </summary>
        public DateTime? DataCriacaoToken { get; private set; }

        /// <summary>
        /// Data de expiração do Token
        /// </summary>
        public DateTime? DataExpiracaoToken { get; private set; }

        public AutenticarUsuarioResponse(bool autenticado, string token, DateTime dataCriacaoToken, DateTime dataExpiracaoToken)
        {
            Autenticado = autenticado;
            Token = token;
            DataCriacaoToken = dataCriacaoToken;
            DataExpiracaoToken = dataExpiracaoToken;
        }

        public AutenticarUsuarioResponse(bool autenticado)
        {
            Autenticado = autenticado;
            Token = string.Empty;
            DataCriacaoToken = null;
            DataExpiracaoToken = null;
        }
    }
}