using apiWebDB.BaseDados.Models;
using apiWebDB.Services.DTOs;
using apiWebDB.Services.Exceptions;
using Microsoft.AspNetCore.Http.Connections;
using System;
using System.Collections.Generic;

namespace apiWebDB.Services.Validate
{
    public class EnderecoValidate
    {
        private static HashSet<string> ValidUFs = new HashSet<string> { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };

        public static bool ValidateUF(string uf)
        {
            if (!ValidUFs.Contains(uf.ToUpper()))
                throw new BadRequestException("UF inválida, informe uma UF do Brasil");
            return true;
        }
        public static bool Execute(EnderecoDTO dto)
        {
            if (dto.Cep <= 0 || dto.Cep.ToString().Length != 8)
                throw new BadRequestException("O Campo CEP deve ter 8 Digitos e não pode ser menor que 0");
            if (string.IsNullOrEmpty(dto.Logradouro))
                throw new InvalidEntityException("Campo Logradouro não pode ser nulo");
            if (dto.Logradouro.Length > 255)
                throw new BadRequestException("Campo Logradouro não pode ultrapassar o limite de 255 caracteres");
            if (string.IsNullOrEmpty(dto.Numero))
                throw new InvalidEntityException("Campo Numero não pode ser nulo");
            if (dto.Numero.Length > 20)
                throw new BadRequestException("Campo Numero não pode ser maior de 20 caracteres");
            if (string.IsNullOrEmpty(dto.Bairro))
                throw new InvalidEntityException("Campo Bairro não pode ser nulo");
            if (dto.Bairro.Length > 100)
                throw new BadRequestException("Campo Bairro não pode ser maior de 100 caracteres");
            if (string.IsNullOrEmpty(dto.Cidade))
                throw new InvalidEntityException("Campo Cidade não pode ser nulo");
            if (dto.Bairro.Length > 255)
                throw new BadRequestException("Campo Cidade não pode ser maior de 255 caracteres");
            if (dto.Clienteid <= 0)
                throw new BadRequestException("Campo Cliente não pode Menor ou Igual a 0");
            if (dto.Status != 0 || dto.Status != 1)
                throw new BadRequestException("Campo Status Inválido");

            return ValidateUF(dto.Uf);
        }

    }
}
