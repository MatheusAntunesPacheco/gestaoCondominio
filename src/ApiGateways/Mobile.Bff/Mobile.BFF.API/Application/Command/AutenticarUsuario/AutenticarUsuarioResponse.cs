namespace Mobile.BFF.API.Application.Command.AutenticarUsuario
{
    /// <summary>
    /// Requisição utilizada para criptografar texto utilizado na api para senha de usuario
    /// </summary>
    public class AutenticarUsuarioResponse
    {
        /// <summary>
        /// Valor que indica se usuário foi autenticado ou não
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

        public AutenticarUsuarioResponse(bool autenticado, string token, DateTime? dataCriacaoToken, DateTime? dataExpiracaoToken)
        {
            Autenticado = autenticado;
            Token = token;
            DataCriacaoToken = dataCriacaoToken;
            DataExpiracaoToken = dataExpiracaoToken;
        }

        public AutenticarUsuarioResponse(bool autenticado)
        {
            Autenticado = autenticado;
        }
    }
}