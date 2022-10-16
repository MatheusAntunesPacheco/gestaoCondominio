namespace Mobile.BFF.API.Services
{
    /// <summary>
    /// Requisição utilizada para criptografar texto utilizado na api para senha de usuario
    /// </summary>
    public class ProcessamentoBaseResponse
    {
        /// <summary>
        /// Indica se o processamento houve sucesso
        /// </summary>
        public bool Sucesso { get; private set; }

        /// <summary>
        /// Mensagem de retorno do processamento, caso necessário
        /// </summary>
        public string Mensagem { get; private set; }
        public ProcessamentoBaseResponse(bool sucesso, string mensagem)
        {

            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }
}