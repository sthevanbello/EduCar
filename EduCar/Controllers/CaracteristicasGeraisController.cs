using EduCar.Interfaces;
using EduCar.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaracteristicasGeraisController : ControllerBase
    {
        private readonly ICaracteristicasGeraisRepository _caracteristicasGeraisRepository;
        public CaracteristicasGeraisController(ICaracteristicasGeraisRepository caracteristicasGeraisRepository)
        {
            _caracteristicasGeraisRepository = caracteristicasGeraisRepository;
        }

        /// <summary>
        /// Exibir uma lista das características dos veículos existentes no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista das características dos veículos</returns>
        [HttpGet]
        public IActionResult GetAllCaracteristicasGerais()
        {
            try
            {
                var caracteristicas = _caracteristicasGeraisRepository.GetAll();
                return Ok(caracteristicas);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar as características do veículo",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir as características a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id das características gerais dos veículos</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna as características gerais</returns>
        [HttpGet("{id}")]
        public IActionResult GetByIdCaracteristicasGerais(int id)
        {
            try
            {
                var caracteristicas = _caracteristicasGeraisRepository.GetById(id);
                if (caracteristicas is null)
                {
                    return NotFound(new { msg = "Características não encontradas. Verifique se o Id está correto" });
                }
                return Ok(caracteristicas);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao exibir as caracteristicas",
                    ex.InnerException.Message
                });
            }
        }
    }
}
