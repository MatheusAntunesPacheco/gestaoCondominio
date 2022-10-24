namespace Agendamento.API.Models
{
    public class ObterAgendamentoResultado
    {
        /// <summary>
        /// Id do condomínio cujo agendamento será realizado
        /// </summary>
        public int IdCondominio { get; private set; }

        /// <summary>
        /// Area a ser reservada
        /// </summary>
        public int IdAreaCondominio { get; private set; }

        /// <summary>
        /// CPF dp usuário
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Data do evento a ser agendado
        /// </summary>
        public DateTime DataEvento { get; private set; }

        /// <summary>
        /// Status do agendamento
        /// </summary>
        public string StatusAgendamento { get; private set; }

        public ObterAgendamentoResultado(int idCondominio, int idAreaCondominio, string cpf, DateTime dataEvento, string statusAgendamento)
        {
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            Cpf = cpf;
            DataEvento = dataEvento;
            StatusAgendamento = statusAgendamento;
        }
    }
}
