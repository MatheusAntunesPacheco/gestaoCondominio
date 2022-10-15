namespace GestaoAcesso.API.Entities
{
    public class PayloadTokenJwt
    {
        /// <summary>
        /// CPF dp usuário
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Atributo que indica se o usuário é administrador do sistema
        /// </summary>
        public bool AdministradorGeral { get; private set; }

        /// <summary>
        /// Lista de Id's de condomínio cujo usuário autenticado é o administrador
        /// </summary>
        public IEnumerable<int> CondominiosAdministrador { get; private set; }

        /// <summary>
        /// Lista de Id's de condomínio cujo usuário autenticado é usuário comum
        /// </summary>
        public IEnumerable<int> CondominiosUsuarioComum { get; private set; }

        /// <summary>
        /// Construtor da entidade PayloadTokenJwt
        /// </summary>
        /// <param name="cpf">CPF do usuário</param>
        /// <param name="nome">Nome do usuário</param>
        public PayloadTokenJwt(string cpf, string nome, bool administradorGeral, IEnumerable<int> condominiosUsuarioAdministrador, IEnumerable<int> condominiosUsuarioComum)
        {
            Cpf = cpf;
            Nome = nome;
            AdministradorGeral = administradorGeral;
            CondominiosAdministrador = condominiosUsuarioAdministrador; 
            CondominiosUsuarioComum = condominiosUsuarioComum;
        }

        public bool UsuarioEhAdministradorCondominio(int idCondominio) => AdministradorGeral || CondominiosAdministrador.Contains(idCondominio);

        public bool UsuarioPertenceACondominio(int idCondominio) => AdministradorGeral || CondominiosAdministrador.Contains(idCondominio) || CondominiosUsuarioComum.Contains(idCondominio);
    }
}
