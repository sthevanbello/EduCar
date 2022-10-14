using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading;
using System.Numerics;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using EduCar.Utils;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Login
    /// Repositório coma a função de efetuar a autenticação do usuário
    /// </summary>
    public class LoginRepository : ILoginRepository
    {
        private readonly EduCarContext _context;
        private readonly IConfiguration _configuration;

        public LoginRepository(EduCarContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        /// <summary>
        /// Método responsável por efetuar o Login do usuário a partir do Json recebido com e-mail e senha
        /// </summary>
        /// <param name="login">Dados do usuário para efetuar o login</param>
        /// <returns>Retorna um string com o Token gerado para autenticação</returns>
        public string Logar(Login login)
        {
            var usuario = _context.Usuario
                .Where(u => u.Email == login.Email)
                .Include(t => t.TipoUsuario)
                .FirstOrDefault();

            if (usuario != null && login.Senha != null && usuario.Senha.Contains("$2b$"))
            {
                bool validPassword = BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha);

                if (validPassword)
                {
                    // Criar as credenciais do JWT

                    // Definições das Claims
                    var minhasClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, usuario.TipoUsuario.Tipo), // Colocar o nível de acesso de acordo com o nível do usuário
                        new Claim("Cargo", usuario.TipoUsuario.Tipo) // Identifica o cargo do usuário
                    };
                    // Criada a chave de criptografia
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("case-chave-autenticacao"));

                    // Criar as credenciais
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Gerar o token (objeto)
                    var token = new JwtSecurityToken(
                        issuer: "case.webAPI",
                        audience: "case.webAPI",
                        claims: minhasClaims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds
                        );
                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
            }
            return null;
        }
        /// <summary>
        /// Solicita o envio do token de troca de senha para o e-mail cadastrado
        /// </summary>
        /// <param name="usuarioEmail">E-mail para solicitar o token para a troca de senha</param>
        /// <returns>Retorna uma mensagem se o e-mail foi enviado com sucesso um null se houve algum erro</returns>
        public object SolicitarTokenSenha(string usuarioEmail)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Email == usuarioEmail);
            if (usuario == null)
            {
                return null;
            }
            var token = BCrypt.Net.BCrypt.HashPassword(usuario.CPF_CNPJ);

            var tokenEmail = Utilidade.GerarToken(token);

            Utilidade.EnviarEmail(_configuration, usuarioEmail, tokenEmail);

            return new
            {
                mensagem = "O e-mail para efetuar a troca de senha foi enviado com sucesso..."
            };
        }

        

        /// <summary>
        /// Efetuar a troca de senha de acordo com o token recebido por e-mail
        /// </summary>
        /// <param name="token">Token recebido por e-mail</param>
        /// <param name="senhaNova">Senha nova a ser cadastrada</param>
        /// <returns>Retorna uma mensagem de sucesso se a senha foi alterada ou um null indicando que houve falha</returns>
        public object TrocarSenha(string token, string senhaNova)
        {
            var resultOne = token.Substring(12);
            var result = resultOne.Substring(0,60);
            var usuarios = _context.Usuario.ToList();
            Usuario usuario = null;
            usuarios.ForEach(user =>
            {
                if (BCrypt.Net.BCrypt.Verify(user.CPF_CNPJ, result))
                {
                    usuario = user;
                }
            });
            if (usuario == null)
            {
                return null;
            }
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(senhaNova);
            _context.Update(usuario);
            _context.SaveChanges();
            return "Senha alterada com sucesso";
        }
    }
}
