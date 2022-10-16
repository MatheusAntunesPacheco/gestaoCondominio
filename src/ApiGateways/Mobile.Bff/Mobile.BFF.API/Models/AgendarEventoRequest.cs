namespace Mobile.BFF.API.Models
{
    public class AgendarEventoRequest
    {
        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Id do condomínio cujo agendamento será realizado
        /// </summary>
        public int IdCondominio { get; private set; }

        /// <summary>
        /// Area a ser reservada
        /// </summary>
        public int IdAreaCondominio { get; private set; }

        /// <summary>
        /// Data do evento a ser agendado
        /// </summary>
        public DateTime DataEvento { get; private set; }

        public AgendarEventoRequest(string cpf, int idCondominio, int idAreaCondominio, DateTime dataEvento)
        {
            Cpf = cpf;
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            DataEvento = dataEvento;
        }
    }
}
