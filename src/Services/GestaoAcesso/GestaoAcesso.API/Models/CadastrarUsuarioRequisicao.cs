namespace GestaoAcesso.API.Models
{
    public class CadastrarUsuarioRequisicao : RequisicaoBase
    {
        /// <summary>
        /// Nome do usuario
        /// </summary>
        public string? Nome { get; set; }

        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string? Cpf { get; set; }

        /// <summary>
        /// Senha do usuario
        /// </summary>
        public string? Senha { get; set; }

        /// <summary>
        /// E-mail do usuario
        /// </summary>
        public string? Email { get; set; }

        protected override void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                AdicionarErro(nameof(Nome), "Campo deve ser preenchido");

            if (string.IsNullOrEmpty(Cpf))
                AdicionarErro(nameof(Cpf), "Campo deve ser preenchido");
            else if (Cpf.Length != 11)
                AdicionarErro(nameof(Cpf), "CPF inválido");

            if (string.IsNullOrEmpty(Senha))
                AdicionarErro(nameof(Senha), "Campo deve ser preenchido");

            if (string.IsNullOrEmpty(Email))
                AdicionarErro(nameof(Email), "Campo deve ser preenchido");
        }
    }
}
