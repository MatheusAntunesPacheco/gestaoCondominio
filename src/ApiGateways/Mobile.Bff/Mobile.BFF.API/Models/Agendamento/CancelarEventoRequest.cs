namespace Mobile.BFF.API.Models.Agendamento
{
    public class CancelarEventoRequest
    {
        /// <summary>
        /// ID do condominio associado a qualquer requisição realizada para a API
        /// </summary>
        public int? IdCondominio { get; set; }

        /// <summary>
        /// Area a ser reservada
        /// </summary>
        public int? IdAreaCondominio { get; set; }

        /// <summary>
        /// Data do evento a ser agendado
        /// </summary>
        public DateTime? DataEvento { get; set; }

        public CancelarEventoRequest(int? idCondominio, int? idAreaCondominio, DateTime? dataEvento)
        {
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            DataEvento = dataEvento;
        }
    }
}
