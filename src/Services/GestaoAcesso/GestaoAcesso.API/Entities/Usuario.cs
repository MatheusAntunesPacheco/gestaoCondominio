using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoAcesso.API.Entities
{
    public class Usuario
    {
        /// <summary>
        /// CPF dp usuário
        /// </summary>
        [Column("txt_cpf")]
        public string Cpf { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        [Column("txt_nome")]
        public string Nome { get; set; }

        /// <summary>
        /// E-mail do usuario
        /// </summary>
        [Column("txt_email")]
        public string Email { get; set; }

        /// <summary>
        /// Senha criptografada salva no banco de dados
        /// </summary>
        [Column("txt_senha")]
        public string SenhaCriptografada { get; set; }

        /// <summary>
        /// Construtor da entidade Usuario
        /// </summary>
        /// <param name="cpf">CPF do usuário</param>
        /// <param name="nome">Nome do usuário</param>
        /// <param name="email">Email do usuário</param>
        /// <param name="senhaCriptografada">Senha preenchida pelo usuário já criptografada</param>
        public Usuario(string cpf, string nome, string email, string senhaCriptografada)
        {
            Cpf = cpf;
            Nome = nome;
            Email = email;
            SenhaCriptografada = senhaCriptografada;
        }
    }
}
