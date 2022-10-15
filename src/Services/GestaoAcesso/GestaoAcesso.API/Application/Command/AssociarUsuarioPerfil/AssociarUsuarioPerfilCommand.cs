using MediatR;

namespace GestaoAcesso.API.Application.Command.AssociarUsuarioPerfil
{
    /// <summary>
    /// Requisição utilizada para criar novo usuario no sistema
    /// </summary>
    public class AssociarUsuarioPerfilCommand : IRequest<ProcessamentoBaseResponse>
    {

        /// <summary>
        /// CPF do usuario
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Id do condomínio associado ao perfil
        /// Caso esss valor seja null, esse usuário é um administrador geral, perfil concedido ao adm do sistema,
        /// tendo permissão de acessar qualquer informação.
        /// </summary>
        public int? IdCondominio { get; private set; }

        /// <summary>
        /// Atributo que indica se o usuário será administrador do condomínio
        /// </summary>
        public bool Administrador { get; private set; }

        /// <summary>
        /// Cpf do usuário que solicitou associação
        /// Apenas usuários administradores do condomínio podem fazer essa associação
        /// </summary>
        public string CpfUsuarioLogado { get; private set; }

        public AssociarUsuarioPerfilCommand(string cpf, int? idCondominio, bool administrador, string cpfUsuarioLogado)
        {
            Cpf = cpf;
            IdCondominio = idCondominio;
            Administrador = administrador;
            CpfUsuarioLogado = cpfUsuarioLogado;
        }
    }
}