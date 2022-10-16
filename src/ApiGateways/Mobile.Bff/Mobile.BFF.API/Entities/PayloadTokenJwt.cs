namespace Mobile.BFF.API.Entities
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
        /// <param name="cpf"></param>
        /// <param name="nome"></param>
        /// <param name="administradorGeral"></param>
        /// <param name="condominiosAdministrador"></param>
        /// <param name="condominiosUsuarioComum"></param>
        public PayloadTokenJwt(string cpf, string nome, bool administradorGeral, IEnumerable<int> condominiosAdministrador, IEnumerable<int> condominiosUsuarioComum)
        {
            Cpf = cpf;
            Nome = nome;
            AdministradorGeral = administradorGeral;
            CondominiosAdministrador = condominiosAdministrador; 
            CondominiosUsuarioComum = condominiosUsuarioComum;
        }

        public bool UsuarioEhAdministradorCondominio(int idCondominio) => AdministradorGeral || CondominiosAdministrador.Contains(idCondominio);

        public bool UsuarioPertenceACondominio(int idCondominio) => AdministradorGeral || CondominiosAdministrador.Contains(idCondominio) || CondominiosUsuarioComum.Contains(idCondominio);

        public bool UsuarioApenasUsuarioComum(int idCondominio) => CondominiosUsuarioComum.Contains(idCondominio);
    }
}
