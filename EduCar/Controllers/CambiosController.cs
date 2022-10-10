using EduCar.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CambiosController : ControllerBase
    {
        private readonly ICambioRepository _cambioRepository;

        public CambiosController(ICambioRepository cambioRepository)
        {
            _cambioRepository = cambioRepository;
        }
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
