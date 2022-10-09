using EduCar.Interfaces;
using EduCar.Models;
using EduCar.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartoesController : ControllerBase
    {
        private readonly ICartaoRepository _cartaoRepository;

        public CartoesController(ICartaoRepository cartaoRepository)
        {
            _cartaoRepository = cartaoRepository;
        }

        /// <summary>
        /// Inserir um cartão no banco.
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="cartao">Cartão a ser inserido</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um cartão inserido ou uma mensagem se houve alguma falha</returns>
        [HttpPost]
        public IActionResult InsertCartao(Cartao cartao)
        {
            try
            {
                var cartaoInserido = _cartaoRepository.Insert(cartao);
                return Ok(cartaoInserido);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao inserir um cartão no banco",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir uma lista de cartões cadastrados no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de cartões</returns>
        [HttpGet]
        public IActionResult GetAllCartoes()
        {
            try
            {
                var cartoes = _cartaoRepository.GetAll();
                return Ok(cartoes);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os cartões",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir um cartão a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id do cartão</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um Cartão</returns>
        [HttpGet("{id}")]
        public IActionResult GetByIdCartao(int id)
        {
            try
            {
                var cartao = _cartaoRepository.GetById(id);
                if (cartao is null)
                {
                    return NotFound(new { msg = "Cartão não foi encontrado. Verifique se o Id está correto" });
                }
                return Ok(cartao);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao exibir o cartão",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Atualizar parte das informações do cartão
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id do cartão</param>
        /// <param name="patchCartao">informações a serem alteradas</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se o cartão foi alterado ou se houve algum erro</returns>
        [HttpPatch("{id}")]
        public IActionResult PatchCartao(int id, [FromBody] JsonPatchDocument patchCartao)
        {
            try
            {
                if (patchCartao is null)
                {
                    return BadRequest(new { msg = "Insira os dados novos" });
                }

                var cartao = _cartaoRepository.GetById(id);
                if (cartao is null)
                {
                    return NotFound(new { msg = "Cartão não encontrado. Conferir o Id informado" });
                }

                _cartaoRepository.Patch(patchCartao, cartao);

                return Ok(new { msg = "Cartão alterado", cartao });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar o cartão",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Alterar um cartão a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id do cartão</param>
        /// <param name="cartao">Dados atualizados</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se o cartão foi alterado ou se houve algum erro</returns>
        [HttpPut("{id}")]
        public IActionResult PutCartao(int id, Cartao cartao)
        {
            try
            {
                if (id != cartao.Id)
                {
                    return BadRequest(new { msg = "Os ids não são correspondentes" });
                }
                var cartaoRetorno = _cartaoRepository.GetById(id);

                if (cartaoRetorno is null)
                {
                    return NotFound(new { msg = "Cartão não encontrado. Conferir o Id informado" });
                }
                
                _cartaoRepository.Put(cartao);

                return Ok(new { msg = "Cartão alterado", cartao });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar o cartão",
                    ex.InnerException.Message
                });
            }
        }
        /// <summary>
        /// Excluir cartão do banco de dados
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id do cartão a ser excluído</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem informando se o cartão foi excluído ou se houve falha</returns>
        [Authorize(Roles = "Master")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCartao(int id)
        {
            try
            {
                var cartaoRetorno = _cartaoRepository.GetById(id);

                if (cartaoRetorno is null)
                {
                    return NotFound(new { msg = "Cartão não encontrado. Conferir o Id informado" });
                }

                _cartaoRepository.Delete(cartaoRetorno);

                return Ok(new { msg = "Cartão excluído com sucesso" });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao excluir o cartão. Verifique se há utilização como Foreign Key.",
                    ex.InnerException.Message
                });
            }
        }
    }
}
