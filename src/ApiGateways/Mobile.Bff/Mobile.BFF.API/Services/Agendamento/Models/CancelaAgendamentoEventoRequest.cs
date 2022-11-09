namespace Mobile.BFF.API.Services.Agendamento.Models
{
    public class CancelaAgendamentoEventoRequest
    {

        /// <summary>
        /// Id do condomínio cujo agendamento será realizado
        /// </summary>
        public int? IdCondominio { get; private set; }

        /// <summary>
        /// Area a ser reservada
        /// </summary>
        public int? IdAreaCondominio { get; private set; }

        /// <summary>
        /// Data do evento a ser agendado
        /// </summary>
        public DateTime? DataEvento { get; set; }

        /// <summary>
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        public string CpfUsuarioLogado { get; private set; }

        public CancelaAgendamentoEventoRequest(int? idCondominio, int? idAreaCondominio, DateTime? dataEvento, string cpfUsuarioLogado)
        {
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            DataEvento = dataEvento;
            CpfUsuarioLogado = cpfUsuarioLogado;
        }
    }
}
