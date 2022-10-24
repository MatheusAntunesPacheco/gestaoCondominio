namespace Agendamento.API.Models
{
    public class CancelarAgendamentoRequisicao : RequisicaoBase
    {
        /// <summary>
        /// ID do condominio associado a qualquer requisição realizada para a API
        /// </summary>
        public int? IdCondominio { get; set; }

        /// <summary>
        /// Area a ser reservada
        /// </summary>
        public int? IdAreaCondominio { get; set; }

        /// <summary>
        /// Data do evento a ser agendado
        /// </summary>
        public DateTime? DataEvento { get; set; }

        /// <summary>
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        public string? CpfUsuarioLogado { get; set; }

        protected override void Validar()
        {
            if (!IdCondominio.HasValue)
                AdicionarErro(nameof(IdCondominio), "Campo deve ser preenchido");

            if (!IdAreaCondominio.HasValue)
                AdicionarErro(nameof(IdAreaCondominio), "Campo deve ser preenchido");

            if (!DataEvento.HasValue)
                AdicionarErro(nameof(DataEvento), "Data deve ser preenchida");

            if (string.IsNullOrEmpty(CpfUsuarioLogado))
                AdicionarErro(nameof(CpfUsuarioLogado), "Campo deve ser preenchido");
            else if (CpfUsuarioLogado.Length != 11)
                AdicionarErro(nameof(CpfUsuarioLogado), "CPF inválido");
        }
    }
}
