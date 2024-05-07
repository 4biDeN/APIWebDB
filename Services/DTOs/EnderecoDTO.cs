using System;

namespace apiWebDB.Services.DTOs
{
    public class EnderecoDTO
    {
        /// <example>89870000</example>
        public int Cep { get; set; }

        /// <example>Avenida Brasília</example>
        public string Logradouro { get; set; }
        
        /// <example>625</example>
        public string Numero { get; set; }
        
        /// <example>Horus Faculdades</example>
        public string Complemento { get; set; }

        /// <example>São José</example>
        public string Bairro { get; set; }
        
        /// <example>Pinhalzinho</example>
        public string Cidade { get; set; }

        /// <example>SC</example>
        public string Uf { get; set; }

        /// <example>1</example>
        public int Clienteid { get; set; }

        /// <summary>
        /// 0 - inativo\n1 - ativo
        /// </summary>
        /// /// <example>1</example>
        public int Status { get; set; }
    }
}
