namespace Agendamento.API.Models
{
    public class AlterarAgendamentoRequisicao : RequisicaoBase
    {
        /// <summary>
        /// Id do condomínio cujo agendamento será realizado
        /// </summary>
        public int? IdCondominio { get; set; }

        /// <summary>
        /// Area a ser reservada
        /// </summary>
        public int? IdAreaCondominio { get; set; }

        /// <summary>
        /// Data do evento
        /// </summary>
        public DateTime? DataAtualEvento { get; set; }

        /// <summary>
        /// Data do evento
        /// </summary>
        public DateTime? NovaDataEvento { get; set; }

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

            if (!DataAtualEvento.HasValue)
                AdicionarErro(nameof(DataAtualEvento), "Campo deve ser preenchido");

            if (!NovaDataEvento.HasValue)
                AdicionarErro(nameof(NovaDataEvento), "Campo deve ser preenchido");

            if (string.IsNullOrEmpty(CpfUsuarioLogado))
                AdicionarErro(nameof(CpfUsuarioLogado), "Campo deve ser preenchido");
            else if (CpfUsuarioLogado.Length != 11)
                AdicionarErro(nameof(CpfUsuarioLogado), "CPF inválido");
        }
    }
}
