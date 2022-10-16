using System.ComponentModel.DataAnnotations.Schema;

namespace Agendamento.API.Entities
{
    public class Agendamento
    {

        /// <summary>
        /// ID do agendamento
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// CPF dp usuário
        /// </summary>
        [Column("txt_cpf")]
        public string Cpf { get; private set; }

        /// <summary>
        /// Id do condomínio cujo agendamento será realizado
        /// </summary>
        [Column("id_condominio")]
        public int IdCondominio { get; private set; }

        /// <summary>
        /// Area a ser reservada
        /// </summary>
        [Column("id_area_condominio")]
        public int IdAreaCondominio { get; private set; }

        /// <summary>
        /// Data do evento a ser agendado
        /// </summary>
        [Column("dt_evento")]
        public DateTime DataEvento { get; set; }

        /// <summary>
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        [Column("txt_cpf_alteracao")]
        public string CpfUsuarioLogado { get; private set; }

        /// <summary>
        /// Data da alteração ou criação do registro de agendamento
        /// </summary>
        [Column("dt_alteracao")]
        public DateTime DataAlteracao { get; set; }

        /// <summary>
        /// Construtor da entidade Usuario
        /// </summary>
        /// <param name="cpf">CPF do usuário</param>
        /// <param name="nome">Nome do usuário</param>
        /// <param name="email">Email do usuário</param>
        /// <param name="senhaCriptografada">Senha preenchida pelo usuário já criptografada</param>
        public Agendamento(string cpf, int idCondominio, int idAreaCondominio, DateTime dataEvento, string cpfUsuarioLogado, DateTime dataAlteracao)
        {
            Cpf = cpf;
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            DataEvento = dataEvento;
            CpfUsuarioLogado = cpfUsuarioLogado;
            DataAlteracao = dataAlteracao;
        }
    }
}
