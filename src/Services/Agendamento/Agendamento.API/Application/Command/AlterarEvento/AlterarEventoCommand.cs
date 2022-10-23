using MediatR;

namespace Agendamento.API.Application.Command.AlterarEvento
{
    /// <summary>
    /// Requisição utilizada para criar novo usuario no sistema
    /// </summary>
    public class AlterarEventoCommand : IRequest<ProcessamentoBaseResponse>
    {

        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string Cpf { get; private set; }

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
        public DateTime DataEvento { get; set; }

        /// <summary>
        /// Cpf do usuário logado, para saber se ele possui permissão para realizar essa associação
        /// </summary>
        public string CpfUsuarioLogado { get; private set; }

        /// <summary>
        /// Atributo que indica se o usuário é administrador do condomínio
        /// </summary>
        public bool UsuarioAdministradorCondominio { get; private set; }

        /// <summary>
        /// Atributo que indica se o usuário é um usuário comum do condomínio
        /// </summary>
        public bool UsuarioComumCondominio { get; private set; }

        public AlterarEventoCommand(string cpf, int idCondominio, int idAreaCondominio, DateTime dataEvento, string cpfUsuarioLogado, bool usuarioAdministradorCondominio, bool usuarioComumCondominio)
        {
            Cpf = cpf;
            IdCondominio = idCondominio;
            IdAreaCondominio = idAreaCondominio;
            DataEvento = dataEvento;
            CpfUsuarioLogado = cpfUsuarioLogado;
            UsuarioAdministradorCondominio = usuarioAdministradorCondominio;
            UsuarioComumCondominio = usuarioComumCondominio;
        }
    }
}