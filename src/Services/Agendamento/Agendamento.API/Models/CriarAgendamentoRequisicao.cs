namespace Agendamento.API.Models
{
    public class CriarAgendamentoRequisicao
    {
        /// <summary>
        /// ID do condominio associado a qualquer requisição realizada para a API
        /// </summary>
        public int IdCondominio { get; private set; }

        /// <summary>
        /// Area a ser reservada
        /// </summary>
        public int IdAreaCondominio { get; private set; }

        /// <summary>
        /// CPF do usuario responsável pelo agendamento
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Data do evento a ser agendado
        /// </summary>
        public DateTime DataEvento { get; private set; }

        /// <summary>
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        public string CpfUsuarioLogado { get; private set; }

        public CriarAgendamentoRequisicao(int idCondominio, int idAreaCondominio, string cpf, DateTime dataEvento, string cpfUsuarioLogado)
        {
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            Cpf = cpf;
            DataEvento = dataEvento;
            CpfUsuarioLogado = cpfUsuarioLogado;
        }
    }
}
