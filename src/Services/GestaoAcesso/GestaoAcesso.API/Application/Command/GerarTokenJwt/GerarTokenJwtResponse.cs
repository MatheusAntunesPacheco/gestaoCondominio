namespace GestaoAcesso.API.Application.Command.GerarTokenJwt
{
    /// <summary>
    /// Requisição utilizada para criptografar texto utilizado na api para senha de usuario
    /// </summary>
    public class GerarTokenJwtResponse
    {
        /// <summary>
        /// Token JWT gerado
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Data de criação do Token
        /// </summary>
        public DateTime DataCriacaoToken { get; private set; }

        /// <summary>
        /// Data de expiração do Token
        /// </summary>
        public DateTime DataExpiracaoToken { get; private set; }

        public GerarTokenJwtResponse(string token, DateTime dataCriacaoToken, DateTime dataExpiracaoToken)
        {
            Token = token;
            DataCriacaoToken = dataCriacaoToken;
            DataExpiracaoToken = dataExpiracaoToken;
        }
    }
}