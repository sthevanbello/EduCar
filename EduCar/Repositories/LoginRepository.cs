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

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Login
    /// Repositório coma a função de efetuar a autenticação do usuário
    /// </summary>
    public class LoginRepository : ILoginRepository
    {
        private readonly EduCarContext _context;

        public LoginRepository(EduCarContext context)
        {
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
    }
}
