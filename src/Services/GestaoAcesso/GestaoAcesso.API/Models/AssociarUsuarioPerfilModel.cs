namespace GestaoAcesso.API.Models
{
    public class AssociarUsuarioPerfilModel
    {
        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// Id do condomínio associado ao perfil
        /// Caso esss valor seja null, esse usuário é um administrador geral, perfil concedido ao adm do sistema,
        /// tendo permissão de acessar qualquer informação.
        /// </summary>
        public int? IdCondominio { get; set; }

        /// <summary>
        /// Atributo que indica se o usuário será administrador do condomínio
        /// </summary>
        public bool Administrador { get; set; }

        /// <summary>
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        public string CpfUsuarioLogado { get; set; }
    }
}
