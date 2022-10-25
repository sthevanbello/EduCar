using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using System.Linq;
using EduCar.Repositories;
using System.Security.Claims;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidosController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Inserir um pedido no banco.
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///          Usuários: Administrador e Cliente 
        /// 
        /// </remarks>
        /// <param name="pedido">Pedido a ser inserido</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um pedido inserido ou uma mensagem se houve alguma falha</returns>
        [Authorize(Roles = "Administrador, Cliente")]
        [HttpPost]
        public IActionResult InsertPedido(Pedido pedido)
        {
            try
            {
                var pedidoInserido = _pedidoRepository.InsertPedidoVenda(pedido);

                if(pedidoInserido is null)
                {
                    return BadRequest(new
                    { 
                        msg = "O veículo escolhido já foi vendido"
                    });
                }

                return Ok(pedidoInserido);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao inserir um pedido no banco",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir uma lista de pedidos cadastrados no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador e Vendedor
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de pedidos</returns>
        [Authorize(Roles = "Administrador, Vendedor")]
        [HttpGet]
        public IActionResult GetAllPedidos()
        {
            try
            {
                var pedidosUsuario = _pedidoRepository.GetPedidosCompletos();
                return Ok(pedidosUsuario);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os pedidos",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir uma lista de pedidos cadastrados no sistema com base no email do usuário
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador, Cliente e Vendedor
        /// 
        /// </remarks>
        /// /// <param name="email">Id do pedido</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de pedidos de um usuário</returns>
        [Authorize(Roles = "Administrador, Cliente, Vendedor")]
        [HttpGet("Usuario/{email}")]
        public IActionResult GetPedidosByEmailUsuario(string email)
        {
            try
            {
                var emailRole = User.Identity.Name;
                if (email.Equals(emailRole) || User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role.ToString()).Value == "Administrador")
                {
                    var pedidosUsuario = _pedidoRepository.GetPedidosByUsuario(email);
                    if (pedidosUsuario is null)
                    {
                        return BadRequest(new
                        {
                            msg = "Não foi possível encontrar um usuário com o email fornecido"
                        });
                    }
                    return Ok(pedidosUsuario);
                }
                return BadRequest(new
                {
                    msg = "O e-mail fornecido não é do mesmo usuário que realizou a autenticação"
                });

            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os pedidos do usuário",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir uma lista de pedidos cadastrados no sistema com base no Id da concessionária
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///            Usuários: Administrador e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id do pedido</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de pedidos de uma concessionária</returns>
        [Authorize(Roles = "Administrador, Vendedor")]
        [HttpGet("Concessionaria/{id}")]
        public IActionResult GetPedidosByIdConcessionaria(int id)
        {
            try
            {
                var pedidosConcessionaria = _pedidoRepository.GetPedidosByConcessionaria(id);

                if (pedidosConcessionaria is null)
                {
                    return BadRequest(new
                    {
                        msg = "Não foi possível encontrar uma concessionária com o id fornecido"
                    });
                }
                return Ok(pedidosConcessionaria);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os pedidos da concessionária",
                    ex.InnerException.Message
                });
            }
        }



        /// <summary>
        /// Atualizar parte das informações do pedido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///          Usuários: Administrador 
        ///          
        /// </remarks>
        /// <param name="id">Id do pedido</param>
        /// <param name="patchPedido">informações a serem alteradas</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se o pedido foi alterado ou se houve algum erro</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPatch("{id}")]
        public IActionResult PatchPedido(int id, [FromBody] JsonPatchDocument patchPedido)
        {
            try
            {
                if (patchPedido is null)
                {
                    return BadRequest(new { msg = "Insira os dados novos" });
                }

                var pedido = _pedidoRepository.GetById(id);
                if (pedido is null)
                {
                    return NotFound(new { msg = "Pedido não encontrado. Conferir o Id informado" });
                }

                _pedidoRepository.Patch(patchPedido, pedido);

                return Ok(new { msg = "Pedido alterado", pedido });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar o pedido",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Alterar um pedido a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///          Usuários: Administrador 
        /// 
        /// </remarks>
        /// <param name="id">Id do pedido</param>
        /// <param name="pedido">Dados atualizados</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se o pedido foi alterado ou se houve algum erro</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult PutPedido(int id, Pedido pedido)
        {
            try
            {
                if (id != pedido.Id)
                {
                    return BadRequest(new { msg = "Os ids não são correspondentes" });
                }
                var pedidoRetorno = _pedidoRepository.GetById(id);

                if (pedidoRetorno is null)
                {
                    return NotFound(new { msg = "Pedido não encontrado. Conferir o Id informado" });
                }

                _pedidoRepository.Put(pedido);

                return Ok(new { msg = "Pedido alterado", pedido });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar o pedido",
                    ex.InnerException.Message
                });
            }
        }
        /// <summary>
        /// Excluir pedido do banco de dados
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///          Usuários: Administrador 
        /// 
        /// </remarks>
        /// <param name="id">Id do pedido a ser excluído</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem informando se o pedido foi excluído ou se houve falha</returns>
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult DeletePedido(int id)
        {
            try
            {
                var pedidoRetorno = _pedidoRepository.GetById(id);

                if (pedidoRetorno is null)
                {
                    return NotFound(new { msg = "Pedido não encontrado. Conferir o Id informado" });
                }

                _pedidoRepository.Delete(pedidoRetorno);

                return Ok(new { msg = "Pedido excluído com sucesso" });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao excluir o pedido. Verifique se há utilização como Foreign Key.",
                    ex.InnerException.Message
                });
            }
        }
    }
}
