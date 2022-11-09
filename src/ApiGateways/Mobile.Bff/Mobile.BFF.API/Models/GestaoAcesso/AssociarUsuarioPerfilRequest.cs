namespace Mobile.BFF.API.Models.GestaoAcesso
{
    public class AssociarUsuarioPerfilRequest
    {
        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Id do condomínio associado ao perfil
        /// Caso esss valor seja null, esse usuário é um administrador geral, perfil concedido ao adm do sistema,
        /// tendo permissão de acessar qualquer informação.
        /// </summary>
        public int? IdCondominio { get; private set; }

        /// <summary>
        /// Atributo que indica se o usuário será administrador do condomínio
        /// </summary>
        public bool Administrador { get; private set; }

        public AssociarUsuarioPerfilRequest(string cpf, int? idCondominio, bool administrador)
        {
            Cpf = cpf;
            IdCondominio = idCondominio;
            Administrador = administrador;
        }
    }
}
