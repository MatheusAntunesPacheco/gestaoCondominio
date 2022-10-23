namespace Agendamento.Domain
{
    public class AgendamentoDomain
    {
        /// <summary>
        /// CPF dp usuário
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
        public DateTime DataEvento { get; set; }

        /// <summary>
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        public string CpfUsuarioLogado { get; private set; }

        /// <summary>
        /// Construtor do agendamento
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="idCondominio"></param>
        /// <param name="idAreaCondominio"></param>
        /// <param name="dataEvento"></param>
        /// <param name="cpfUsuarioLogado"></param>
        public AgendamentoDomain(string cpf, int idCondominio, int idAreaCondominio, DateTime dataEvento, string cpfUsuarioLogado)
        {
            Cpf = cpf;
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            DataEvento = dataEvento;
            CpfUsuarioLogado = cpfUsuarioLogado;
        }
    }
}
