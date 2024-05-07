using apiWebDB.BaseDados.Models;
using System.Collections.Generic;
using System;

namespace apiWebDB.Services.DTOs
{
    public class ClienteDTO
    {
        /// <example>
        /// Cristian Müller
        /// </example>
        public string Nome { get; set; }

        /// <example>
        /// 2002-04-11
        /// </example>
        public DateOnly? Nascimento { get; set; }

        /// <example>
        /// 49998203752
        /// </example>
        public string Telefone { get; set; }

        /// <example>
        /// 10328233994
        /// </example>
        public string Documento { get; set; }

        /// <summary>
        /// 0 - CPF 1 - CNPJ 2 - Passaporte 3 - CNH 99 - Outros
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        public int Tipodoc { get; set; }
    }
}
