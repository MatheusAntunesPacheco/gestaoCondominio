using Agendamento.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agendamento.Infrastructure.Model
{
    public class AgendamentoModel
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
        public DateTime DataEvento { get; private set; }

        /// <summary>
        /// Status do agendamento
        /// 1 - Agendado
        /// 2 - Cancelado
        /// </summary>
        [Column("id_status")]
        public StatusAgendamentoEnum StatusAgendamento { get; private set; }

        /// <summary>
        /// Data do agendamento
        /// </summary>
        [Column("dt_alteracao")]
        public DateTime DataAlteracao { get; private set; }

        /// <summary>
        /// Data do agendamento
        /// </summary>
        [Column("txt_cpf_alteracao")]
        public string CpfAlteracao { get; private set; }

        /// <summary>
        /// Constrotor do Agendamento 
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="idCondominio"></param>
        /// <param name="idAreaCondominio"></param>
        /// <param name="dataEvento"></param>
        /// <param name="statusAgendamento"></param>
        /// <param name="dataAlteracao"></param>
        /// <param name="cpfAlteracao"></param>
        public AgendamentoModel(string cpf, int idCondominio, int idAreaCondominio, DateTime dataEvento, StatusAgendamentoEnum statusAgendamento, DateTime dataAlteracao, string cpfAlteracao)
        {
            Cpf = cpf;
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            DataEvento = dataEvento;
            StatusAgendamento = statusAgendamento;
            DataAlteracao = dataAlteracao;
            CpfAlteracao = cpfAlteracao;
        }

        public void AlterarStatusAgendamento(string cpfAlteracao, StatusAgendamentoEnum idStatus)
        {
            StatusAgendamento = idStatus;
            CpfAlteracao = cpfAlteracao;
            DataAlteracao = DateTime.UtcNow;
        }

        public void AlterarDataEvento(string cpfAlteracao, DateTime novaDataEvento)
        {
            DataEvento = novaDataEvento;
            CpfAlteracao = cpfAlteracao;
            DataAlteracao = DateTime.UtcNow;
        }
    }
}
