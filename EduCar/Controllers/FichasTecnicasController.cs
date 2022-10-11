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
    public class FichasTecnicasController : ControllerBase
    {
        private readonly IFichaTecnicaRepository _fichaTecnicaRepository;

        public FichasTecnicasController(IFichaTecnicaRepository fichaTecnicaRepository)
        {
            _fichaTecnicaRepository = fichaTecnicaRepository;
        }


        /// <summary>
        /// Exibir uma lista de fichas técnicas cadastrados no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///          Usuários: Administrador, Cliente e Vendedor
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de fichas técnicas</returns>
        [Authorize(Roles = "Administrador, Cliente, Vendedor")]
        [HttpGet]
        public IActionResult GetAllFichasTecnicas()
        {
            try
            {
                var fichasTecnicas = _fichaTecnicaRepository.GetAll();
                return Ok(fichasTecnicas);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar as fichas técnicas",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir uma ficha técnica a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///          Usuários: Administrador, Cliente e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id da ficha técnica</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma ficha técnica</returns>
        [Authorize(Roles = "Administrador, Cliente, Vendedor")]
        [HttpGet("{id}")]
        public IActionResult GetByIdFichaTecnica(int id)
        {
            try
            {
                var fichaTecnica = _fichaTecnicaRepository.GetById(id);
                if (fichaTecnica is null)
                {
                    return NotFound(new { msg = "Ficha técnica não foi encontrada. Verifique se o Id está correto" });
                }
                return Ok(fichaTecnica);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao exibir a ficha técnica",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Atualizar parte das informações da ficha técnica
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id da ficha técnica</param>
        /// <param name="patchFichaTecnica">informações a serem alteradas</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se a ficha técnica foi alterado ou se houve algum erro</returns>
        [Authorize(Roles = "Administrador, Vendedor")]
        [HttpPatch("{id}")]
        public IActionResult PatchFichaTecnica(int id, [FromBody] JsonPatchDocument patchFichaTecnica)
        {
            try
            {
                if (patchFichaTecnica is null)
                {
                    return BadRequest(new { msg = "Insira os dados novos" });
                }

                var fichaTecnica = _fichaTecnicaRepository.GetById(id);
                if (fichaTecnica is null)
                {
                    return NotFound(new { msg = "Ficha técnica não encontrado. Conferir o Id informado" });
                }

                _fichaTecnicaRepository.Patch(patchFichaTecnica, fichaTecnica);

                return Ok(new { msg = "Ficha técnica alterada", fichaTecnica });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar a ficha técnica",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Alterar uma ficha técnica a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id da ficha técnica</param>
        /// <param name="fichaTecnica">Dados atualizados</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se a ficha técnica foi alterada ou se houve algum erro</returns>
        [Authorize(Roles = "Administrador, Vendedor")]
        [HttpPut("{id}")]
        public IActionResult PutFichaTecnica(int id, FichaTecnica fichaTecnica)
        {
            try
            {
                if (id != fichaTecnica.Id)
                {
                    return BadRequest(new { msg = "Os ids não são correspondentes" });
                }
                var fichaTecnicaRetorno = _fichaTecnicaRepository.GetById(id);

                if (fichaTecnicaRetorno is null)
                {
                    return NotFound(new { msg = "Ficha técnica não encontrada. Conferir o Id informado" });
                }
                
                _fichaTecnicaRepository.Put(fichaTecnica);

                return Ok(new { msg = "Ficha técnica alterado", fichaTecnica });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar a ficha técnica",
                    ex.InnerException.Message
                });
            }
        }
        ///// <summary>
        ///// Excluir ficha técnica do banco de dados
        ///// </summary>
        ///// <remarks>
        ///// 
        ///// Acesso permitido:
        ///// 
        /////       Usuários: Administrador e Vendedor
        ///// 
        ///// </remarks>
        ///// <param name="id">Id da ficha técnica a ser excluído</param>
        ///// <response code="401">Acesso negado</response>
        ///// <response code="403">Nível de acesso não está autorizado</response>
        ///// <returns>Retorna uma mensagem informando se a ficha técnica foi excluído ou se houve falha</returns>
        //[Authorize(Roles = "Administrador, Vendedor")]
        //[HttpDelete("{id}")]
        //public IActionResult DeleteFichaTecnica(int id)
        //{
        //    try
        //    {
        //        var fichaTecnicaRetorno = _fichaTecnicaRepository.GetById(id);

        //        if (fichaTecnicaRetorno is null)
        //        {
        //            return NotFound(new { msg = "Ficha técnica não encontrada. Conferir o Id informado" });
        //        }

        //        _fichaTecnicaRepository.Delete(fichaTecnicaRetorno);

        //        return Ok(new { msg = "Ficha técnica excluída com sucesso" });
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(new
        //        {
        //            msg = "Falha ao excluir a ficha técnica. Verifique se há utilização como Foreign Key.",
        //            ex.InnerException.Message
        //        });
        //    }
        //}
    }
}
