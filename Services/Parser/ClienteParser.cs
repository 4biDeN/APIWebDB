using apiWebDB.BaseDados.Models;
using apiWebDB.Services.DTOs;
using System;

namespace apiWebDB.Services.Parser
{
    public static class ClienteParser
    {
        public static TbCliente ToEntity(ClienteDTO dto)
        {
            var time = new TimeOnly(0, 0);
            var nascimento = new DateTime(
                (DateOnly)dto.Nascimento, time);

            return new TbCliente
            {
                Nome = dto.Nome,
                Nascimento = nascimento,
                Telefone = dto.Telefone,
                Tipodoc = dto.Tipodoc,
                Documento = dto.Documento,
                Criadoem = DateTime.Now,
                Alteradoem = DateTime.Now,
            };
        }
    }
}