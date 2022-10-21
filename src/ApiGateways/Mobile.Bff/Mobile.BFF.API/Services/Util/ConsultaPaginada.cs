namespace Mobile.BFF.API.Services.Agendamento.Models
{
    public class ConsultaPaginada<T>
    {
        /// <summary>
        /// Pagina a ser exibida
        /// </summary>
        public int Pagina { get; private set; }

        /// <summary>
        /// Tamanho da pagina a ser exibida
        /// </summary>
        public int TamanhoPagina { get; private set; }

        /// <summary>
        /// Quantidade total de registros
        /// </summary>
        public int QuantidadeTotalIens { get; private set; }

        /// <summary>
        /// Lista de itens paginados
        /// </summary>
        public IEnumerable<T> Itens { get; private set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="tamanhoPagina"></param>
        /// <param name="quantidadeTotalIens"></param>
        /// <param name="itens"></param>
        public ConsultaPaginada(int pagina, int tamanhoPagina, int quantidadeTotalIens, IEnumerable<T> itens)
        {
            Pagina = pagina;
            TamanhoPagina = tamanhoPagina;
            QuantidadeTotalIens = quantidadeTotalIens;
            Itens = itens;
        }
    }
}
