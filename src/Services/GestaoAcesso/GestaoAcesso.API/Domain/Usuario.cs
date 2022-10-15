namespace GestaoAcesso.API.Domain
{
    public class Usuario
    {
        public string Cpf { get; set; }
        public IEnumerable<Perfil> ListaPerfis{ get; set; }

        /// <summary>
        /// Construtor da entidade Usuario
        /// </summary>
        /// <param name="cpf">CPF do usuário</param>
        public Usuario(string cpf, IEnumerable<Perfil> listaPerfis)
        {
            Cpf = cpf;
            ListaPerfis = listaPerfis;
        }

        public bool UsuarioAdministradorGeral { get { return ListaPerfis.Any(p => p.PerfilAdministradorGeral); } }
        public bool UsuarioAdministradorCondominio(int idCondominio) => UsuarioAdministradorGeral || ListaPerfis.Any(p => p.PerfilAdministradorCondominio(idCondominio));
    }
}
