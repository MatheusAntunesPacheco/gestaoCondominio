﻿using MediatR;

namespace GestaoAcesso.API.Application.Command.GerarTokenJwt
{
    /// <summary>
    /// Requisição utilizada para gerar token JWT para autenticação do usuário
    /// </summary>
    public class GerarTokenJwtCommand : IRequest<GerarTokenJwtResponse>
    {
        /// <summary>
        /// Atributo que identifica se usuário foi autenticado
        /// </summary>
        public bool Autenticado { get; private set; }

        /// <summary>
        /// CPF dp usuário
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Atributo que indica se o usuário é administrador do sistema
        /// </summary>
        public bool AdministradorGeral { get; private set; }

        /// <summary>
        /// Lista de Id's de condomínio cujo usuário autenticado é o administrador
        /// </summary>
        public IEnumerable<int> CondominiosAdministrador { get; private set; }

        /// <summary>
        /// Lista de Id's de condomínio cujo usuário autenticado é usuário comum
        /// </summary>
        public IEnumerable<int> CondominiosUsuarioComum { get; private set; }

        public GerarTokenJwtCommand(string cpf, string nome, bool administradorGeral, IEnumerable<int> condominiosAdministrador, IEnumerable<int> condominiosUsuarioComum)
        {
            Cpf = cpf;
            Nome = nome;
            AdministradorGeral = administradorGeral;
            CondominiosAdministrador = condominiosAdministrador;
            CondominiosUsuarioComum = condominiosUsuarioComum;
        }
    }
}