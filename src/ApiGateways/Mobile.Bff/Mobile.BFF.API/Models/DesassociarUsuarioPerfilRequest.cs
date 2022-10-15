namespace Mobile.BFF.API.Models
{
    public class DesassociarUsuarioPerfilRequest
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

        public DesassociarUsuarioPerfilRequest(string cpf, int? idCondominio)
        {
            Cpf = cpf;
            IdCondominio = idCondominio;
        }
    }
}
