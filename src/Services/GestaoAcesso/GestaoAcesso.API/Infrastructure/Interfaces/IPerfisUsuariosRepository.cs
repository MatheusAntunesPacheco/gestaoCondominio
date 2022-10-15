﻿using GestaoAcesso.API.Entities;

namespace GestaoAcesso.API.Infrastructure.Interfaces
{
    public interface IPerfisUsuariosRepository
    {
        Task<PerfilUsuario> Criar(PerfilUsuario usuario);
        IEnumerable<PerfilUsuario> ListarPorCpf(string cpf);
    }
}