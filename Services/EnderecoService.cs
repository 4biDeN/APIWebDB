using apiWebDB.BaseDados;
using apiWebDB.BaseDados.Models;
using apiWebDB.Services.DTOs;
using apiWebDB.Services.Exceptions;
using apiWebDB.Services.Parser;
using apiWebDB.Services.Validate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;

namespace apiWebDB.Services
{
    public class EnderecoService
    {
        private readonly ApidbContext _dbcontext;

        public EnderecoService(ApidbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public TbEndereco GetById(int id)
        {
            var existingEntity = _dbcontext.TbEnderecos.FirstOrDefault(c => c.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException("Registro não existe");
            }
            return existingEntity;
        }
        public IEnumerable<TbEndereco> Get()
        {
            var existingEntity = _dbcontext.TbEnderecos.ToList();

            if (existingEntity == null || existingEntity.Count == 0)
            {
                throw new NotFoundException("Nenhum registro encontrado");
            }
            return existingEntity;
        }
        public TbEndereco Insert(EnderecoDTO dto) 
        {
            if (!EnderecoValidate.Execute(dto))
                return null;

            var entity = EnderecoParser.ToEntity(dto);

            _dbcontext.Add(entity);
            _dbcontext.SaveChanges();

            return entity;
        }
        public TbEndereco Update(int id, EnderecoDTO dto)
        {
            var existingEntity = GetById(id);

            if (existingEntity == null)
                throw new NotFoundException("Id de Cliente Não Existe");

            if (!EnderecoValidate.Execute(dto))
                return null;

            var entity = EnderecoParser.ToEntity(dto);
            var endereco = GetById(id);

            endereco.Cep = entity.Cep;
            endereco.Logradouro = entity.Logradouro;
            endereco.Numero = entity.Numero;
            endereco.Complemento = entity.Complemento;
            endereco.Bairro = entity.Bairro;
            endereco.Cidade = entity.Cidade;
            endereco.Uf = entity.Uf;
            endereco.Clienteid = entity.Clienteid;
            endereco.Status = entity.Status;

            _dbcontext.Update(endereco);
            _dbcontext.SaveChanges();

            return entity;
        }
        public void Delete(int id)
        {
            var existingEntity = GetById(id);

            if (existingEntity == null)
                throw new NotFoundException("Registro não existe");

            _dbcontext.Remove(existingEntity);
            _dbcontext.SaveChanges();
        }
    }

}
