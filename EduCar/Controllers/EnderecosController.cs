using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using EduCar.Repositories;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecosController : ControllerBase
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecosController(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        /// <summary>
        /// Inserir um endereço no banco.
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="endereco">Endereço a ser inserido</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um endereço inserido ou uma mensagem se houve alguma falha</returns>
        [HttpPost]
        public IActionResult InsertEndereco(Endereco endereco)
        {
            try
            {
                var enderecoInserido = _enderecoRepository.Insert(endereco);
                return Ok(enderecoInserido);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao inserir um endereço no banco",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir uma lista de endereços cadastrados no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de Endereços</returns>
        [HttpGet]
        public IActionResult GetAllEnderecos()
        {
            try
            {
                var enderecos = _enderecoRepository.GetAll();
                return Ok(enderecos);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os endereços",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir um endereço a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id do endereço</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um Endereço</returns>
        [HttpGet("{id}")]
        public IActionResult GetByIdEndereco(int id)
        {
            try
            {
                var endereco = _enderecoRepository.GetById(id);
                if (endereco is null)
                {
                    return NotFound(new { msg = "Endereço não foi encontrado. Verifique se o Id está correto" });
                }
                return Ok(endereco);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao exibir o endereço",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Atualizar parte das informações do endereço
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id do endereço</param>
        /// <param name="patchEndereco">informações a serem alteradas</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se o endereço foi alterado ou se houve algum erro</returns>
        [HttpPatch("{id}")]
        public IActionResult PatchEndereco(int id, [FromBody] JsonPatchDocument patchEndereco)
        {
            try
            {
                if (patchEndereco is null)
                {
                    return BadRequest(new { msg = "Insira os dados novos" });
                }

                var endereco = _enderecoRepository.GetById(id);
                if (endereco is null)
                {
                    return NotFound(new { msg = "Endereço não encontrado. Conferir o Id informado" });
                }

                _enderecoRepository.Patch(patchEndereco, endereco);

                return Ok(new { msg = "Endereço alterado", endereco });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar o endereço",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Alterar um endereço a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id do endereço</param>
        /// <param name="endereco">Dados atualizados</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se o endereço foi alterado ou se houve algum erro</returns>
        [HttpPut("{id}")]
        public IActionResult PutEndereco(int id, Endereco endereco)
        {
            try
            {
                if (id != endereco.Id)
                {
                    return BadRequest(new { msg = "Os ids não são correspondentes" });
                }
                var enderecoRetorno = _enderecoRepository.GetById(id);

                if (enderecoRetorno is null)
                {
                    return NotFound(new { msg = "Endereço não encontrado. Conferir o Id informado" });
                }

                _enderecoRepository.Put(endereco);

                return Ok(new { msg = "Endereço alterado", endereco });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar o endereço",
                    ex.InnerException.Message
                });
            }
        }
        /// <summary>
        /// Excluir endereço do banco de dados
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id do endereço a ser excluído</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem informando se o endereço foi excluído ou se houve falha</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteEndereco(int id)
        {
            try
            {
                var enderecoRetorno = _enderecoRepository.GetById(id);

                if (enderecoRetorno is null)
                {
                    return NotFound(new { msg = "Endereço não encontrado. Conferir o Id informado" });
                }

                _enderecoRepository.Delete(enderecoRetorno);

                return Ok(new { msg = "Endereço excluído com sucesso" });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao excluir o endereço. Verifique se há utilização como Foreign Key.",
                    ex.InnerException.Message
                });
            }
        }
    }
}
