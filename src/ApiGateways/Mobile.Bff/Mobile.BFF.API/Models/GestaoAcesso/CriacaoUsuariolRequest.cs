namespace Mobile.BFF.API.Models.GestaoAcesso
{
    public class CriacaoUsuariolRequest
    {
        /// <summary>
        /// Nome do usuario
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Senha do usuario
        /// </summary>
        public string Senha { get; private set; }

        /// <summary>
        /// E-mail do usuario
        /// </summary>
        public string Email { get; private set; }

        public CriacaoUsuariolRequest(
            string nome,
            string cpf,
            string senha,
            string email)
        {
            Nome = nome;
            Cpf = cpf;
            Senha = senha;
            Email = email;
        }
    }
}
