using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcessionariasController : ControllerBase
    {
        private readonly IConcessionariaRepository _concessionariaRepository;

        public ConcessionariasController(IConcessionariaRepository concessionariaRepository)
        {
            _concessionariaRepository = concessionariaRepository;
        }

        /// <summary>
        /// Inserir uma concessionária no banco.
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador, Vendedor
        /// 
        /// </remarks>
        /// <param name="concessionaria">Concessionária a ser inserida</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma concessionária inserida ou uma mensagem se houve alguma falha</returns>
        [Authorize(Roles = "Administrador, Vendedor")]
        [HttpPost]
        public IActionResult InsertConcessionaria(Concessionaria concessionaria)
        {
            try
            {
                var concessionariaInserida = _concessionariaRepository.Insert(concessionaria);
                return Ok(concessionariaInserida);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao inserir uma concessionária no banco",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir uma lista de concessionárias cadastradas no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador, Cliente e Vendedor
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de concessionárias</returns>
        [Authorize(Roles = "Administrador, Cliente, Vendedor")]
        [HttpGet]
        public IActionResult GetAllConcessionarias()
        {
            try
            {
                var concessionarias = _concessionariaRepository.GetAll();
                return Ok(concessionarias);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os concessionárias",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir uma concessionária a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador, Cliente e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id da concessionárias</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma Concessionária</returns>
        [Authorize(Roles = "Administrador, Cliente, Vendedor")]
        [HttpGet("{id}")]
        public IActionResult GetByIdConcessionaria(int id)
        {
            try
            {
                var concessionaria = _concessionariaRepository.GetById(id);
                if (concessionaria is null)
                {
                    return NotFound(new { msg = "Concessionária não foi encontrada. Verifique se o Id está correto" });
                }
                return Ok(concessionaria);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao exibir a concessionária",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Atualizar parte das informações da concessionária
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id da concessionária</param>
        /// <param name="patchConcessionaria">informações a serem alteradas</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se a concessionária foi alterada ou se houve algum erro</returns>
        [Authorize(Roles = "Administrador, Vendedor")]
        [HttpPatch("{id}")]
        public IActionResult PatchConcessionaria(int id, [FromBody] JsonPatchDocument patchConcessionaria)
        {
            try
            {
                if (patchConcessionaria is null)
                {
                    return BadRequest(new { msg = "Insira os dados novos" });
                }

                var concessionaria = _concessionariaRepository.GetById(id);
                if (concessionaria is null)
                {
                    return NotFound(new { msg = "Concessionária não encontrada. Conferir o Id informado" });
                }

                _concessionariaRepository.Patch(patchConcessionaria, concessionaria);

                return Ok(new { msg = "Concessionária alterada", concessionaria });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar a concessionária",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Alterar uma concessionária a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///            Usuários: Administrador e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id da concessionária</param>
        /// <param name="concessionaria">Dados atualizados</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se o concessionária foi alterada ou se houve algum erro</returns>
        [Authorize(Roles = "Administrador, Vendedor")]
        [HttpPut("{id}")]
        public IActionResult PutConcessionaria(int id, Concessionaria concessionaria)
        {
            try
            {
                if (id != concessionaria.Id)
                {
                    return BadRequest(new { msg = "Os ids não são correspondentes" });
                }
                var cpncessionariaRetorno = _concessionariaRepository.GetById(id);

                if (cpncessionariaRetorno is null)
                {
                    return NotFound(new { msg = "Concessionária não encontrada. Conferir o Id informado" });
                }

                _concessionariaRepository.Put(concessionaria);

                return Ok(new { msg = "Concessionária alterada", concessionaria });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar a concessionária",
                    ex.InnerException.Message
                });
            }
        }
        /// <summary>
        /// Excluir concessionária do banco de dados
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///             Usuários: Administrador e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id da concessionária a ser excluído</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem informando se o concessionária foi excluída ou se houve falha</returns>
        [Authorize(Roles = "Administrador, Vendedor")]
        [HttpDelete("{id}")]
        public IActionResult DeleteConcessionaria(int id)
        {
            try
            {
                var concessionariaRetorno = _concessionariaRepository.GetById(id);

                if (concessionariaRetorno is null)
                {
                    return NotFound(new { msg = "Concessionária não encontrada. Conferir o Id informado" });
                }

                _concessionariaRepository.Delete(concessionariaRetorno);

                return Ok(new { msg = "Concessionária excluída com sucesso" });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao excluir a concessionária. Verifique se há utilização como Foreign Key.",
                    ex.InnerException.Message
                });
            }
        }
    }
}
