namespace Agendamento.API.Models
{
    public class ModelBase
    {
        /// <summary>
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        public string CpfUsuarioLogado { get; private set; }

        /// <summary>
        /// ID do condominio associado a qualquer requisição realizada para a API
        /// </summary>
        public int IdCondominio { get; private set; }

        /// <summary>
        /// Atributo que indica se o usuário é administrador do condomínio
        /// </summary>
        public bool UsuarioAdministradorCondominio { get; private set; }

        /// <summary>
        /// Atributo que indica se o usuário é um usuário comum do condomínio
        /// </summary>
        public bool UsuarioComumCondominio { get; private set; }

        public ModelBase(string cpfUsuarioLogado, int idCondominio, bool usuarioAdministradorCondominio, bool usuarioComumCondominio)
        {
            CpfUsuarioLogado = cpfUsuarioLogado;
            IdCondominio = idCondominio;
            UsuarioAdministradorCondominio = usuarioAdministradorCondominio;
            UsuarioComumCondominio = usuarioComumCondominio;
        }
    }
}
