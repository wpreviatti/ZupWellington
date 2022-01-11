using CryptoWPS;
using EFClient.Dominio;
using EFClient.Repo;
using EFClient.WebApi.Library;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFClient.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IEFCoreRepository _repo;

        public ClienteController(IEFCoreRepository repo)
        {
            _repo = repo;
        }
        //se sobrar tempo verificar handleexeption no startp
        /// <summary>
        /// Obtem todos os clientes cadastrados na base! para performance serão trazido apenas os primeiros 200
        /// </summary>
        /// <returns>Retorna a lista de clientes</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var clients = await _repo.GetAll();
                return Ok(clients);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var cliente = await _repo.GetById(id);
                if (cliente != null)
                {
                    return Ok(cliente);
                }
                return BadRequest("Não foi possivel obter o valor com o id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Salva os valores informados na base.
        /// </summary>
        /// <param name="value">Objeto de cliente populado</param>
        /// <returns></returns>
        // POST api/<ClienteController>
        [HttpPost]
        public async Task<ActionResult> Post(Cliente value)
        {
            try
            {
                #region validação basica
                if (value == null)
                    throw new Exception(string.Format("Valor inválido para ser salvo na base de dados! Verifique a documentação e tente novamente"));
                #endregion
                #region aplica criptografia
                value = CustomClientPrepare.CriptografaCliente(value);
                #endregion
                value.NumeroChapaId = 0;
                _repo.Add(value);
                if (await _repo.SaveChangeAsync())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Não foi possivel salvar");
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var cliente = await _repo.GetById(id);
                if (cliente != null)
                {
                    _repo.Deletar(cliente);
                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok();
                    }
                }
                return BadRequest("Não foi possivel Deletar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<testeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Cliente value)
        {
            try
            {
                var cliente = await _repo.GetById(id);
                if (cliente != null)
                {
                    value.NumeroChapaId = cliente.NumeroChapaId;
                    #region aplica criptografia
                    value = CustomClientPrepare.CriptografaCliente(value);
                    #endregion
                    _repo.Update(value);
                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok(value);
                    }
                }
                return BadRequest("Não foi possivel fazer o update");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
