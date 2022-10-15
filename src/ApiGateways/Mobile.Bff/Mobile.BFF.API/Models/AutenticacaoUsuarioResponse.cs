namespace Mobile.BFF.API.Models
{
    public class AutenticacaoUsuarioResponse
    {
        /// <summary>
        /// Atributo que identifica se usuário foi autenticado
        /// </summary>
        public bool Autenticado { get; private set; }

        /// <summary>
        /// Data de criação do Token
        /// </summary>
        public DateTime? DataCriacaoToken { get; private set; }

        /// <summary>
        /// Data de expiração do token
        /// </summary>
        public DateTime? DataExpiracaoToken { get; private set; }

        /// <summary>
        /// Token JWT
        /// </summary>
        public string Token { get; private set; }

        public AutenticacaoUsuarioResponse(bool autenticado, DateTime? dataCriacaoToken, DateTime? dataExpiracaoToken, string token)
        {
            Autenticado = autenticado;
            DataCriacaoToken = dataCriacaoToken;
            DataExpiracaoToken = dataExpiracaoToken;
            Token = token;
        }
    }
}
