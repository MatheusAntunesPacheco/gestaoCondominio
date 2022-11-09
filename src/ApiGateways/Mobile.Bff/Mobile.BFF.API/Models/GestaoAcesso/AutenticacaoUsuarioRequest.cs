namespace Mobile.BFF.API.Models.GestaoAcesso
{
    public class AutenticacaoUsuarioRequest
    {
        /// <summary>
        /// CPF do usuário a ser autenticado
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Senha do usuário a ser autenticado
        /// </summary>
        public string Senha { get; private set; }

        public AutenticacaoUsuarioRequest(string cpf, string senha)
        {
            Cpf = cpf;
            Senha = senha;
        }
    }
}
