﻿namespace Agendamento.API.Models
{
    public class CriarAgendamentoRequisicao : RequisicaoBase
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
        /// CPF do usuario responsável pelo agendamento
        /// </summary>
        public string? Cpf { get; set; }

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

            if (string.IsNullOrEmpty(Cpf))
                AdicionarErro(nameof(Cpf), "Campo deve ser preenchido");
            else if (Cpf.Length != 11)
                AdicionarErro(nameof(Cpf), "CPF inválido");

            if (!DataEvento.HasValue)
                AdicionarErro(nameof(DataEvento), "Campo deve ser preenchido");

            if (string.IsNullOrEmpty(CpfUsuarioLogado))
                AdicionarErro(nameof(CpfUsuarioLogado), "Campo deve ser preenchido");
            else if (CpfUsuarioLogado.Length != 11)
                AdicionarErro(nameof(CpfUsuarioLogado), "CPF inválido");
        }
    }
}
