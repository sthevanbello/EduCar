using EduCar.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuariosController : ControllerBase
    {
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

        public TipoUsuariosController(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }

        /// <summary>
        /// Exibir uma lista de Tipos de usuários cadastrados no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de tipoUsuario ou se houve falha</returns>
        [HttpGet]
        public IActionResult GetAllTipoUsuario()
        {
            try
            {
                var tipoUsuarios = _tipoUsuarioRepository.GetAll();
                return Ok(tipoUsuarios);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os tipos de usuários",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir um tipo de usuário a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <param name="id">Id do tipo de usuário</param>
        /// <returns>Retorna um tipoUsuario ou se houve falha</returns>
        [HttpGet("{id}")]
        public IActionResult GetByIdTipoUsuario(int id)
        {
            try
            {
                var tipoUsuario = _tipoUsuarioRepository.GetById(id);
                if (tipoUsuario is null)
                {
                    return NotFound(new { msg = "Tipo de usuário não foi encontrado. Verifique se o Id está correto" });
                }
                return Ok(tipoUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao exibir o tipo de usuário",
                    ex.InnerException.Message
                });
            }
        }
    }
}
