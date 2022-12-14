using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using System.Linq;
using System.Security.Claims;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Inserir um usuário cliente no banco.
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///          Qualquer pessoa
        /// 
        /// </remarks>
        /// <param name="usuario">Usuário cliente a ser inserido</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um usuário cliente inserido ou uma mensagem se houve alguma falha</returns>
        [AllowAnonymous]
        [HttpPost("Cliente")]
        public IActionResult InsertUsuarioCliente(Usuario usuario)
        {
            try
            {
                if (!usuario.Aceite)
                {
                    return BadRequest(new 
                    { 
                        msg = "Os termos não foram aceitos e por isso o usuário não foi criado" 
                    });
                }
                // Criptografa a senha
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                usuario.IdTipoUsuario = 1; // Força a inserção de um usuário do tipo cliente
                var usuarioInserido = _usuarioRepository.Insert(usuario);
                return Ok(usuarioInserido);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao inserir um usuário cliente no banco",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Inserir um usuário vendedor no banco.
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///          Usuários: Administrador
        /// 
        /// </remarks>
        /// <param name="usuario">Usuário vendedor a ser inserido</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um usuário vendedor inserido ou uma mensagem se houve alguma falha</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPost("Vendedor")]
        public IActionResult InsertUsuarioVendedor(Usuario usuario)
        {
            try
            {
                // Criptografa a senha
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                usuario.IdTipoUsuario = 2; // Força a inserção de um usuário do tipo vendedor
                var usuarioInserido = _usuarioRepository.Insert(usuario);
                return Ok(usuarioInserido);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao inserir um usuário vendedor no banco",
                    ex.InnerException.Message
                });
            }
        }
        /// <summary>
        /// Inserir um usuário administrador no banco.
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///          Usuários: Administrador
        /// 
        /// </remarks>
        /// <param name="usuario">Usuário administrador a ser inserido</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um usuário administrador inserido ou uma mensagem se houve alguma falha</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPost("Administrador")]
        public IActionResult InsertUsuarioAdministrador(Usuario usuario)
        {
            try
            {
                // Criptografa a senha
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                usuario.IdTipoUsuario = 3; // Força a inserção de um usuário do tipo Administrador
                var usuarioInserido = _usuarioRepository.Insert(usuario);
                return Ok(usuarioInserido);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao inserir um usuário administrador no banco",
                    ex.InnerException.Message
                });
            }
        }
        /// <summary>
        /// Exibir uma lista de usuários cadastrados no sistema
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
        /// <returns>Retorna uma lista de usuários</returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult GetAllUsuarios()
        {
            try
            {
                var usuarios = _usuarioRepository.GetAll();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os usuários",
                    ex.InnerException.Message
                });
            }
        }
        /// <summary>
        /// Exibir um usuário a partir do e-mail fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador, Cliente e Vendedor
        /// 
        /// </remarks>
        /// <param name="email">E-mail do usuário</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um Usuário</returns>
        [Authorize(Roles = "Administrador, Cliente, Vendedor")]
        [HttpGet("{email}")]
        public IActionResult GetByEmailUsuario(string email)
        {
            try
            {
                var emailRole = User.Identity.Name;
                if (email.Equals(emailRole) || User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role.ToString()).Value == "Administrador")
                {
                    var usuario = _usuarioRepository.GetByEmailUsuario(email);
                    if (usuario is null)
                    {
                        return NotFound(new { msg = "Usuário não foi encontrado. Verifique se o e-mail está correto" });
                    }
                    return Ok(usuario);
                }
                return BadRequest(new { msg = "O e-mail informado não corresponde ao e-mail utilizado na autenticação" });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao exibir o usuário",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Atualizar parte das informações do usuário
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador, Cliente e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id do usuário</param>
        /// <param name="patchUsuario">informações a serem alteradas</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se o usuário foi alterado ou se houve algum erro</returns>
        [Authorize(Roles = "Administrador, Cliente, Vendedor")]
        [HttpPatch("{id}")]
        public IActionResult PatchUsuario(int id, [FromBody] JsonPatchDocument patchUsuario)
        {
            try
            {
                if (patchUsuario is null)
                {
                    return BadRequest(new { msg = "Insira os dados novos" });
                }

                var usuario = _usuarioRepository.GetById(id);
                if (usuario is null)
                {
                    return NotFound(new { msg = "Usuário não encontrado. Conferir o Id informado" });
                }

                _usuarioRepository.Patch(patchUsuario, usuario);

                return Ok(new { msg = "Usuário alterado", usuario });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar o usuário",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Alterar um usuário a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///           Usuários: Administrador, Cliente e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id do usuário</param>
        /// <param name="usuario">Dados atualizados</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se o usuário foi alterado ou se houve algum erro</returns>
        [Authorize(Roles = "Administrador, Cliente, Vendedor")]
        [HttpPut("{id}")]
        public IActionResult PutUsuario(int id, Usuario usuario)
        {
            try
            {
                if (id != usuario.Id)
                {
                    return BadRequest(new { msg = "Os ids não são correspondentes" });
                }
                var usuarioRetorno = _usuarioRepository.GetById(id);

                if (usuarioRetorno is null)
                {
                    return NotFound(new { msg = "Usuário não encontrado. Conferir o Id informado" });
                }

                // Criptografa a senha
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                _usuarioRepository.Put(usuario);

                return Ok(new { msg = "Usuário alterado", usuario });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar o usuário",
                    ex.InnerException.Message
                });
            }
        }
        /// <summary>
        /// Excluir Usuário do banco de dados
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        ///            Usuários: Administrador, Cliente e Vendedor
        /// 
        /// </remarks>
        /// <param name="id">Id do usuário a ser excluído</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem informando se o usuário foi excluído ou se houve falha</returns>
        [Authorize(Roles = "Administrador, Cliente, Vendedor")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            try
            {
                var usuarioRetorno = _usuarioRepository.GetById(id);

                if (usuarioRetorno is null)
                {
                    return NotFound(new { msg = "Usuário não encontrado. Conferir o Id informado" });
                }

                _usuarioRepository.Delete(usuarioRetorno);

                return Ok(new { msg = "Usuário excluído com sucesso" });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao excluir o usuário. Verifique se há utilização como Foreign Key.",
                    ex.InnerException.Message
                });
            }
        }
    }
}
