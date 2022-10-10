using EduCar.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirecoesController : ControllerBase
    {
        private readonly IDirecaoRepository _direcaoRepository;

        public DirecoesController(IDirecaoRepository direcaoRepository)
        {
            _direcaoRepository = direcaoRepository;
        }
        [HttpGet]
        public IActionResult GetDirecoes()
        {
            try
            {
                var direcoes = _direcaoRepository.GetAll();

                return Ok(direcoes);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Msg = "Falha ao exibir a lista de direções", ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdDirecao(int id)
        {
            try
            {
                var direcao = _direcaoRepository.GetById(id);
                if (direcao == null)
                {
                    return NotFound(new { Msg = "Tipo de direção não encontrada" });
                }
                return Ok(direcao);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Msg = "Falha ao exibir tipo de direção", ex.Message });
            }
        }
    }
}
