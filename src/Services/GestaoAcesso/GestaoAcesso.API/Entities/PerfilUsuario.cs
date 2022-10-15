﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoAcesso.API.Entities
{
    public class PerfilUsuario
    {
        /// <summary>
        /// Id do perfil
        /// </summary>
        [Column("id")]
        public int Id { get; private set; }

        /// <summary>
        /// CPF dp usuário
        /// </summary>
        [Column("txt_cpf")]
        public string Cpf { get; private set; }

        /// <summary>
        /// ID do condomínio cujo usuário está associado
        /// </summary>
        [Column("id_condominio")]
        public int? IdCondominio { get; private set; }

        /// <summary>
        /// Atributo que indica se usuário é administrador do condomínio associado
        /// </summary>
        [Column("idc_adm")]
        public bool Administrador { get; private set; }

        public PerfilUsuario(int id, string cpf, int? idCondominio, bool administrador)
        {
            Id = id;
            Cpf = cpf;
            IdCondominio = idCondominio;
            Administrador = administrador;
        }
    }
}