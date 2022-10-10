using EduCar.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusVendaController : ControllerBase
    {
        private readonly IStatusVendaRepository _statusVendaRepository;

        public StatusVendaController(IStatusVendaRepository statusVendaRepository)
        {
            _statusVendaRepository = statusVendaRepository;
        }
        /// <summary>
        /// Exibir uma lista de status existentes no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de Status</returns>
        [HttpGet]
        public IActionResult GetAllStatus()
        {
            try
            {
                var status = _statusVendaRepository.GetAll();
                return Ok(status);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os status",
                    ex.InnerException.Message
                });
            }
        }
    }
}
