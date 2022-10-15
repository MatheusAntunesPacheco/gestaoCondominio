namespace Mobile.BFF.API.Services.Models
{
    public class DesassociacaoUsuarioPerfilRequest
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
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        public string CpfUsuarioLogado { get; private set; }

        public DesassociacaoUsuarioPerfilRequest(string cpf, int? idCondominio, string cpfUsuarioLogado)
        {
            Cpf = cpf;
            IdCondominio = idCondominio;
            CpfUsuarioLogado = cpfUsuarioLogado;
        }
    }
}
