using apiWebDB.BaseDados.Models;
using apiWebDB.Services;
using apiWebDB.Services.DTOs;
using apiWebDB.Services.Exceptions;
using apiWebDB.Services.Parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Mime;

namespace apiWebDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _service;
        private readonly ILogger<EnderecoController> _logger;

        public EnderecoController(EnderecoService service, ILogger<EnderecoController> Logger)
        {
            _service = service;
            _logger = Logger;
        }
        /// <summary>
        /// Faz a INserção de um Endereço
        /// </summary>
        /// <returns>Retorna o Endereço Cadastrado</returns>
        /// <response code="200">Returna o endereço ataulizado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="422">Entidade INválida</response>
        [HttpPost()]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TbEndereco))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<TbEndereco> Insert(EnderecoDTO endereco)
        {
            try
            {
                var entity = _service.Insert(endereco);
                return Ok(endereco);
            }
            catch (InvalidEntityException E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 422
                };
            }
            catch (BadRequestException E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 400
                };
            }
            catch (System.Exception E)
            {
                _logger.LogError(E.Message);
                return BadRequest(E.Message);
            }
        }
        /// <summary>
        /// Faz a Atualização de um endereço
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna o Endereço atualizado</returns>
        /// <response code="200">Returna o endereço ataulizado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="404">Endereço não encontrado</response>
        /// <response code="422">Entidade INválida</response>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TbEndereco))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TbEndereco> Update(int id, EnderecoDTO dto)
        {
            try
            {
                var updatedEntity = _service.Update(id, dto);
                return Ok(updatedEntity);
            }
            catch (NotFoundException E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 404
                };
            }
            catch (InvalidEntityException E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 422
                };
            }
            catch (BadRequestException E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 400
                };
            }
            catch (System.Exception E)
            {
                _logger.LogError(E.Message);
                return BadRequest(E.Message);
            }
        }

        /// <summary>
        /// Faz a Exclusão de um Endereço de acordo com seu ID
        /// </summary>
        /// <returns>Retorna o Endereço Deletado</returns>
        /// <response code="200">Retorna o Endereço Deletado</response>
        /// <response code="404">Endereço não encontrado</response>
        /// <response code="500">Erro no Servidor</response>
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TbEndereco> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (NotFoundException E)
            {
                _logger.LogError(E.Message);
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
        /// Lista o Endereço pelo se ID.
        /// </summary>
        /// <returns>Retorna o endereço cadastrado no ID</returns>
        /// <response code="200">Returna o endereço cadastrado</response>
        /// <response code="404">Endereço não encontrado</response>
        /// <response code="500">Erro no Servidor</response>
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TbEndereco))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TbEndereco> GetById(int id)
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
        /// Lista todos os Endereços.
        /// </summary>
        /// <returns>Retorna os endereços cadastrados</returns>
        /// <response code="200">Returna os endereços cadastrados</response>
        /// <response code="404">Endereços não encontrados</response>
        /// <response code="500">Erro no Servidor</response>
        [HttpGet()]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TbEndereco>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TbEndereco> Get()
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
