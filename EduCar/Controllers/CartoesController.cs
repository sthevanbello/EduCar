using EduCar.Interfaces;
using EduCar.Models;
using EduCar.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;

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
        /// Exibir uma lista de cartões cadastrados no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de cartões</returns>
        [Authorize(Roles = "Administrador")]
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
        /// Exibir um cartão cadastrados no sistema de acordo com o id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador e Cliente
        /// 
        /// </remarks>
        /// <param name="email">Email do titular dos cartões</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um cartão cadastrados no sistema de acordo com o id fornecido</returns>
        [Authorize(Roles = "Administrador, Cliente")]
        [HttpGet("{email}")]
        public IActionResult GetByEmailCartao(string email)
        {
            try
            {
                var emailRole = User.Identity.Name;
                if (email.Equals(emailRole) || User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.Role.ToString()).Value == "Administrador")
                {
                    var cartoes = _cartaoRepository.GetByEmailCartoes(email);
                    return Ok(cartoes);
                }
                return BadRequest(new { msg = "O e-mail informado não corresponde ao e-mail utilizado na autenticação" });
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
    }
}
