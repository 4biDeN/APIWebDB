using apiWebDB.BaseDados.Models;
using apiWebDB.Services;
using apiWebDB.Services.DTOs;
using apiWebDB.Services.Exceptions;
using apiWebDB.Services.Parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace apiWebDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly clientesService _service;
        private readonly ILogger<ClientesController> _logger;
        public ClientesController(clientesService service, ILogger<ClientesController> Logger)
        {
            _service = service;
            _logger = Logger;
        }
        /// <summary>
        /// Faz a Inserção de um Cliente
        /// </summary>
        /// <returns>Retorna o Endereço atualizado</returns>
        /// <response code="200">Returna o Cliente Cadastrado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="422">Entidade INválida</response>
        [HttpPost()]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TbEndereco))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<TbCliente> Insert(ClienteDTO cliente)
        {
            try
            {
                var entity = _service.Insert(cliente);
                return Ok(cliente);
            }
            catch (InvalidEntityException E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 422
                };
            }
            catch (System.Exception E)
            {
                _logger.LogError(E.Message);
                return BadRequest(E.Message);
            }
        }
        /// <summary>
        /// Faz a Atualização de um Cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna o Cliente Atualizado</returns>
        /// <response code="200">Returna o Cliente ataulizado</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TbEndereco))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TbCliente> Update(int id, ClienteDTO dto)
        {
            try
            {
                var updatedEntity = _service.Update(id, dto);
                return Ok(updatedEntity);
            }
            catch (System.Exception E)
            {
                _logger.LogError(E.Message);
                return BadRequest(E.Message);
            }
        }

        /// <summary>
        /// Faz a Exclusão de um Cliente de acordo com seu ID
        /// </summary>
        /// <returns>Retorna o Cliente Deletado</returns>
        /// <response code="200">Retorna o Cleinte Deletado</response>
        /// <response code="404">Cliente não encontrado</response>
        /// <response code="500">Erro no Servidor</response>
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TbCliente> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (NotFoundException E)
            {
                return NotFound(E.Message);
            }
            catch (System.Exception E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 500
                };
            }
        }
        /// <summary>
        /// Lista o Cliente pelo se ID.
        /// </summary>
        /// <returns>Retorna o Cliente cadastrado no ID</returns>
        /// <response code="200">Returna o Cliente cadastrado</response>
        /// <response code="404">Cliente não encontrado</response>
        /// <response code="500">Erro no Servidor</response>
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TbEndereco))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TbCliente> GetById(int id)
        {
            try
            {
                var entity = _service.GetById(id);
                return Ok(entity);
            }
            catch (NotFoundException E)
            {
                return NotFound(E.Message);
            }
            catch (System.Exception E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 500
                };
            }
        }
        /// <summary>
        /// Lista todos os Clientes Cadastrados.
        /// </summary>
        /// <returns>Retorna os Todos os Clientes cadastrados</returns>
        /// <response code="200">Returna os Todos os Clientes cadastrados</response>
        /// <response code="404">Clientes não encontrados</response>
        /// <response code="500">Erro no Servidor</response>
        [HttpGet()]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TbEndereco>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TbCliente> Get()
        {
            try
            {
                var entity = _service.Get();
                return Ok(entity);
            }
            catch (NotFoundException E)
            {
                return NotFound(E.Message);
            }
            catch (System.Exception E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 500
                };
            }
        }
    }
}