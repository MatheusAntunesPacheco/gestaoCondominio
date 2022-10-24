using MediatR;

namespace Agendamento.API.Application.Command.AlterarEvento
{
    /// <summary>
    /// Requisição utilizada para criar novo usuario no sistema
    /// </summary>
    public class AlterarEventoCommand : IRequest<ProcessamentoBaseResponse>
    {
        /// <summary>
        /// Id do condomínio cujo agendamento será realizado
        /// </summary>
        public int IdCondominio { get; private set; }

        /// <summary>
        /// Area a ser reservada
        /// </summary>
        public int IdAreaCondominio { get; private set; }

        /// <summary>
        /// Data do evento a ser agendado
        /// </summary>
        public DateTime DataAtualEvento { get; private set; }

        /// <summary>
        /// Nova data do evento
        /// </summary>
        public DateTime NovaDataEvento { get; private set; }

        /// <summary>
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        public string CpfUsuarioLogado { get; private set; }

        public AlterarEventoCommand(int idCondominio, int idAreaCondominio, DateTime dataAtualEvento, DateTime novaDataEvento, string cpfUsuarioLogado)
        {
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            DataAtualEvento = dataAtualEvento;
            NovaDataEvento = novaDataEvento;
            CpfUsuarioLogado = cpfUsuarioLogado;
        }
    }
}