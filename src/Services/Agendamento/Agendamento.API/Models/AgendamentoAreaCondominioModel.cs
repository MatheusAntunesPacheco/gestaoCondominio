namespace Agendamento.API.Models
{
    public class AgendamentoAreaCondominioModel : ModelBase
    {
        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Area a ser reservada
        /// </summary>
        public int IdAreaCondominio { get; private set; }

        /// <summary>
        /// Data do evento a ser agendado
        /// </summary>
        public DateTime DataEvento { get; private set; }

        public AgendamentoAreaCondominioModel(string cpf, int idCondominio, int idAreaCondominio, string cpfUsuarioLogado, DateTime dataEvento, bool usuarioAdministradorCondominio, bool usuarioComumCondominio) : 
            base(cpfUsuarioLogado, idCondominio, usuarioAdministradorCondominio, usuarioComumCondominio)
        {
            Cpf = cpf;
            this.IdAreaCondominio = idAreaCondominio;
            DataEvento = dataEvento;
        }
    }
}
