namespace Mobile.BFF.API.Services.Agendamento.Models
{
    public class AgendamentoEventoResult
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
        public DateTime DataEvento { get; set; }

        /// <summary>
        /// Status do agendamento
        /// </summary>
        public string StatusAgendamento { get; private set; }

        /// <summary>
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        public string CpfUsuarioLogado { get; private set; }

        /// <summary>
        /// Data da alteração ou criação do registro de agendamento
        /// </summary>
        public DateTime DataAlteracao { get; private set; }

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cpf"></param>
        /// <param name="idCondominio"></param>
        /// <param name="idAreaCondominio"></param>
        /// <param name="dataEvento"></param>
        /// <param name="cpfUsuarioLogado"></param>
        /// <param name="dataAlteracao"></param>
        public AgendamentoEventoResult(int idCondominio, int idAreaCondominio, string cpf, DateTime dataEvento, string statusAgendamento, string cpfUsuarioLogado, DateTime dataAlteracao)
        {
            Cpf = cpf;
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            DataEvento = dataEvento;
            StatusAgendamento = statusAgendamento;
            CpfUsuarioLogado = cpfUsuarioLogado;
            DataAlteracao = dataAlteracao;
        }
    }
}
