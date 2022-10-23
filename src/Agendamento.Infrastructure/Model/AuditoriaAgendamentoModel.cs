using System.ComponentModel.DataAnnotations.Schema;

namespace Agendamento.Infrastructure.Model
{
    public class AuditoriaAgendamentoModel
    {

        /// <summary>
        /// ID do registro de auditoria do agendamento
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Id do Agendamento
        /// </summary>
        [Column("id_agendamento")]
        public int IdAgendamento { get; private set; }

        /// <summary>
        /// Id do status
        /// </summary>
        [Column("id_status")]
        public int IdStatus { get; private set; }

        /// <summary>
        /// Data do evento com o agendamento alterado
        /// </summary>
        [Column("dt_evento")]
        public DateTime DataEvento { get; private set; }

        /// <summary>
        /// Cpf do usuário que realizou alteração no agendamento
        /// </summary>
        [Column("txt_cpf_alteracao")]
        public string CpfUsuarioAlteracao { get; private set; }

        /// <summary>
        /// Data de alteração do agendamento
        /// </summary>
        [Column("dt_alteracao")]
        public DateTime DataAlteracao { get; private set; }

        /// <summary>
        /// Construtor da classe de auditoria do agendamento
        /// </summary>
        /// <param name="idAgendamento"></param>
        /// <param name="idStatus"></param>
        /// <param name="dataEvento"></param>
        /// <param name="cpfUsuarioAlteracao"></param>
        /// <param name="dataAlteracao"></param>
        public AuditoriaAgendamentoModel(int idAgendamento, int idStatus, DateTime dataEvento, string cpfUsuarioAlteracao, DateTime dataAlteracao)
        {
            IdAgendamento = idAgendamento;
            IdStatus = idStatus;
            DataEvento = dataEvento;
            CpfUsuarioAlteracao = cpfUsuarioAlteracao;
            DataAlteracao = dataAlteracao;
        }
    }
}
