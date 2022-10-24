using MediatR;

namespace Agendamento.API.Application.Command.AgendarEvento
{
    /// <summary>
    /// Requisição utilizada para criar novo usuario no sistema
    /// </summary>
    public class AgendarEventoCommand : IRequest<ProcessamentoBaseResponse>
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
        /// CPF do usuario responsável pela reserva
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Data do evento a ser agendado
        /// </summary>
        public DateTime DataEvento { get; private set; }

        /// <summary>
        /// Cpf do usuário logado
        /// </summary>
        public string CpfUsuarioLogado { get; private set; }

        public AgendarEventoCommand(int idCondominio, int idAreaCondominio, string cpf, DateTime dataEvento, string cpfUsuarioLogado)
        {
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            Cpf = cpf;
            DataEvento = dataEvento;
            CpfUsuarioLogado = cpfUsuarioLogado;
        }
    }
}