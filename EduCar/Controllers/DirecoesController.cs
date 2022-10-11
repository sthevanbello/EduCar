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
        /// <summary>
        /// Exibir uma lista de tipos de direções existentes no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de tipos de direções</returns>
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
        /// <summary>
        /// Exibir um tipo de direção existente no sistema de acordo com o id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id da direção contida no sistema</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma direção contida no sistema</returns>
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
