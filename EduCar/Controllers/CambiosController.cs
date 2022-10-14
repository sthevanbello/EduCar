using EduCar.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EduCar.Controllers
{
    [Authorize(Roles = "Administrador, Cliente, Vendedor")]
    [Route("api/[controller]")]
    [ApiController]
    public class CambiosController : ControllerBase
    {
        private readonly ICambioRepository _cambioRepository;

        public CambiosController(ICambioRepository cambioRepository)
        {
            _cambioRepository = cambioRepository;
        }
        /// <summary>
        /// Exibir uma lista de tipos de câmbios existentes no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///         Usuários: Administrador, Cliente e Vendedor
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de tipos de câmbios</returns>        

        [HttpGet]
        public IActionResult GetCambios()
        {
            try
            {
                var cambios = _cambioRepository.GetAll();

                return Ok(cambios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Msg = "Falha ao exibir a lista de cambios", ex.Message });
            }
        }
        /// <summary>
        /// Exibir um tipo de câmbio existente no sistema de acordo com o id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        ///         Usuários: Administrador, Cliente e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id do câmbio contido no sistema</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um tipo de câmbios</returns>
        [HttpGet("{id}")]
        public IActionResult GetByIdCambio(int id)
        {
            try
            {
                var cambio = _cambioRepository.GetById(id);
                if (cambio == null)
                {
                    return NotFound(new { Msg = "Tipo cambio não encontrado" });
                }
                return Ok(cambio);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Msg = "Falha ao exibir o tipo de cambio", ex.Message });
            }


        }
    }
}
