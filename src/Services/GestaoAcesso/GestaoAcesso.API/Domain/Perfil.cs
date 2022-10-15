namespace GestaoAcesso.API.Domain
{
    public class Perfil
    {

        /// <summary>
        /// ID do condomínio cujo usuário está associado
        /// </summary>
        public int? IdCondominio { get; private set; }

        /// <summary>
        /// Atributo que indica se usuário é administrador do condomínio associado
        /// </summary>
        public bool Administrador { get; private set; }

        /// <summary>
        /// Construtor da entidade Usuario
        /// </summary>
        /// <param name="cpf">CPF do usuário</param>
        /// <param name="nome">Nome do usuário</param>
        /// <param name="email">Email do usuário</param>
        /// <param name="senhaCriptografada">Senha preenchida pelo usuário já criptografada</param>
        public Perfil(int? idCondominio, bool administrador)
        {
            IdCondominio = idCondominio;
            Administrador = administrador;
        }


        public bool PerfilAdministradorGeral { get { return Administrador && !IdCondominio.HasValue; } }
        public bool PerfilAdministradorCondominio(int idCondominio) => PerfilAdministradorGeral || (Administrador && IdCondominio.HasValue && IdCondominio.Value == idCondominio);
    }
}
