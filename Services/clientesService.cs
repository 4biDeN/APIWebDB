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
using System.Runtime.CompilerServices;

namespace apiWebDB.Services
{
    public class clientesService
    {
        private readonly ApidbContext _dbcontext;
        public clientesService(ApidbContext DbContext)
        {
            _dbcontext = DbContext;

        }
        public TbCliente GetById(int id)
        {
            var existingEntity = _dbcontext.TbClientes.FirstOrDefault(c => c.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException("Registro não existe");
            }
            return existingEntity;
        }
        public IEnumerable<TbCliente> Get()
        {
            var existingEntity = _dbcontext.TbClientes.ToList();
            if (existingEntity == null || existingEntity.Count == 0)
            {
                throw new NotFoundException("Nenhum registro encontrado");
            }
            return existingEntity;
        }
        public TbCliente Insert(ClienteDTO dto)
        {
            if (!ClienteValidate.Execute(dto))
                return null;

            var entity = ClienteParser.ToEntity(dto);

            _dbcontext.Add(entity);
            _dbcontext.SaveChanges();

            return entity;
        }
        public TbCliente Update(int id, ClienteDTO dto)
        {
            if (!ClienteValidate.Execute(dto))
                return null;

            var existingEntity = GetById(id);

            if (existingEntity == null)
                throw new InvalidEntityException("TESTE");

            var entity = ClienteParser.ToEntity(dto);
            var cliente = GetById(id);

            cliente.Nome = entity.Nome;
            cliente.Nascimento = entity.Nascimento;
            cliente.Telefone = entity.Telefone;
            cliente.Tipodoc = entity.Tipodoc;
            cliente.Documento = entity.Documento;
            cliente.Alteradoem = DateTime.Now;

            _dbcontext.Update(cliente);
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
