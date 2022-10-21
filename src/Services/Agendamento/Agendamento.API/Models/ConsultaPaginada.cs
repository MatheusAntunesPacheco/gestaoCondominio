namespace Agendamento.API.Models
{
    public class ConsultaPaginada<T>
    {
        public int Pagina { get; private set; }
        public int TamanhoPagina { get; private set; }
        public int QuantidadeTotalIens { get; private set; }
        public IEnumerable<T> Itens { get; private set; }

        public ConsultaPaginada(int pagina, int tamanhoPagina, int quantidadeTotalIens, IEnumerable<T> itens)
        {
            Pagina = pagina;
            TamanhoPagina = tamanhoPagina;
            QuantidadeTotalIens = quantidadeTotalIens;
            Itens = itens;
        }
    }
}
