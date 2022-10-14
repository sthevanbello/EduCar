using EduCar.Interfaces;
using EduCar.Models;
using EduCar.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginsController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        /// <summary>
        /// Realizar login do usuário
        /// </summary>
        /// <remarks>
        /// 
        ///     Insira o e-mail e a senha para realizar a autenticação da API
        ///     
        /// Exemplo: 
        /// 
        /// Cliente:
        /// 
        ///     {
        ///        "email": "luigi@email.com",
        ///        "senha": "123456789"
        ///     }
        ///     
        /// Vendedor:
        /// 
        ///     {
        ///        "email": "carrosystem@email.com",
        ///        "senha": "123456789"
        ///     }
        ///     
        /// Administrador:
        /// 
        ///     {
        ///        "email": "fernanda@email.com",
        ///        "senha": "123456789"
        ///     }
        /// 
        /// </remarks>
        /// <param name="login">Informações de login do usuário</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um token de acesso ou uma mensagem se houve alguma falha</returns>
        [HttpPost("Token")]
        public IActionResult Logar(Login login)
        {
            try
            {
                var retorno = _loginRepository.Logar(login);

                if(retorno is null)
                {
                    return BadRequest(new
                    {
                        msg = "Email ou senha inválidos"
                    });
                }

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao realizar o login",
                    ex.InnerException.Message
                });
            }
        }
        /// <summary>
        /// Solicitar um token para recuperação de senha
        /// </summary>
        /// <param name="email">E-mail cadastrado no sistema</param>
        /// <returns>Retorna uma mensagem de sucesso ou de falha ao solicitar a recuperação de senha </returns>
        [HttpPost("Senha/Esquecimento")]
        public IActionResult SolicitarTokenDeTrocaDeSenha(string email)
        {
            try
            {
                var retorno = _loginRepository.SolicitarTokenSenha(email);

                if (retorno is null)
                {
                    return BadRequest(new
                    {
                        msg = "O e-mail fornecido não foi encontrado"
                    });
                }

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao solicitar a recuperação de senha",
                    ex.InnerException.Message
                });
            }
        }
        /// <summary>
        /// Efetuar a troca da senha a partir de um token recebido por e-mail
        /// </summary>
        /// <param name="token">Token recebido por e-mail</param>
        /// <param name="senhaNova">Senha nova a ser atualizada</param>
        /// <returns>Retorna uma mensagem de sucesso ou de falha ao solicitar a troca da senha</returns>
        [HttpPost("Senha/Trocar")]
        public IActionResult TrocarSenha(string token, string senhaNova )
        {
            try
            {
                var retorno = _loginRepository.TrocarSenha(token, senhaNova);

                if (retorno is null)
                {
                    return BadRequest(new
                    {
                        msg = "Não foi possível trocar a senha, verifique se o token está correto"
                    });
                }

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao realizar a troca da senha",
                    ex.InnerException.Message
                });
            }
        }
    }
}
