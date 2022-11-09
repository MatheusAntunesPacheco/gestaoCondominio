namespace Mobile.BFF.API.Models.Agendamento
{
    public class ListarEventoRequest
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
    }
}
