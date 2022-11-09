namespace Mobile.BFF.API.Models
{
    public class BaseRequest
    {
        /// <summary>
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        public string? CpfUsuarioLogado { get; set; }
    }
}
