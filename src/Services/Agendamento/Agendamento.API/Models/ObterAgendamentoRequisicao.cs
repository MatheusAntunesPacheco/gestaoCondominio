namespace Agendamento.API.Models
{
    public class ObterAgendamentoRequisicao : RequisicaoBase
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
        /// Data inicio da pesquisa
        /// </summary>
        public DateTime? DataInicio { get; set; }

        /// <summary>
        /// Data fim da pesquisa
        /// </summary>
        public DateTime? DataFim { get; set; }

        /// <summary>
        /// Indicador para verificar se retorno pode listar agendamentos cancelados
        /// </summary>
        public bool ConsultarAgendamentosCancelados { get; set; }

        /// <summary>
        /// Pagina da consulta
        /// </summary>
        public int Pagina { get; set; }

        /// <summary>
        /// Tamanho da pagina da consulta
        /// </summary>
        public int TamanhoPagina { get; set; }

        protected override void Validar()
        {
            if (!IdCondominio.HasValue && !DataInicio.HasValue && !DataFim.HasValue)
                AdicionarErro("Obrigatorio preencher o ID do condominio ou alguma data da consulta");

            if (DataInicio.HasValue && DataFim.HasValue && DataFim.Value <= DataInicio.Value)
                AdicionarErro("Data fim deve ser maior que data inicio da consulta");

            if (Pagina == 0)
                AdicionarErro("Numero da pagina deve ser maior que 0");

            if (TamanhoPagina == 0)
                AdicionarErro("Tamanho da pagina deve ser maior que 0");
        }
    }
}
