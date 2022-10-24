namespace GestaoAcesso.API.Models
{
    public class AutenticarUsuarioRequisicao : RequisicaoBase
    {
        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string? Cpf { get; set; }

        /// <summary>
        /// Senha do usuario
        /// </summary>
        public string? Senha { get; set; }

        protected override void Validar()
        {
            if (string.IsNullOrEmpty(Cpf))
                AdicionarErro(nameof(Cpf), "Campo deve ser preenchido");
            else if (Cpf.Length != 11)
                AdicionarErro(nameof(Cpf), "CPF inválido");
        }
    }
}
