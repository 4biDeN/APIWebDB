using apiWebDB.BaseDados.Models;
using apiWebDB.Services.DTOs;
using apiWebDB.Services.Exceptions;
using Microsoft.AspNetCore.Http.Connections;
using System;

namespace apiWebDB.Services.Validate
{
    public class ClienteValidate
    {
        private static bool ValidateDocumento(TipoDocumento tipo, string documento)
        {
            switch (tipo)
            {
                case TipoDocumento.CPF:
                    if (documento.Length != 11)
                        throw new BadRequestException("O CPF deve ter 11 digitos");
                    return true;
                case TipoDocumento.CNPJ:
                    if (documento.Length != 14)
                        throw new BadRequestException("O CNPJ deve ter 14 digitos");
                    return true;
                case TipoDocumento.Pssaporte:
                    if (documento.Length != 8)
                        throw new BadRequestException("O Passaporte deve ter 8 digitos");
                    return true;
                case TipoDocumento.CNH:
                    if (documento.Length != 11)
                        throw new BadRequestException("O CPF deve ter 11 digitos");
                    return true;
                default:
                    return true;
            }
        }
        public static bool Execute(ClienteDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nome))
                throw new InvalidEntityException("Campo Nome é Obrigatório.");

            if (string.IsNullOrEmpty(dto.Documento))
                throw new InvalidEntityException("Campo Documento é obrigatório.");
            if (dto.Tipodoc <= 0)
                throw new InvalidEntityException("Campo TipoDoc é obrigatório.");

            TipoDocumento tipo;
            try
            {
                tipo = (TipoDocumento)dto.Tipodoc;

            }
            catch
            {
                throw new InvalidEntityException($"O TipoDoc {dto.Tipodoc} é inválido.");
            }

            return ValidateDocumento(tipo, dto.Documento);

        }
    }
}