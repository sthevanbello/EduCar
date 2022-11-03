using System.Linq;
using System;
using Azure.Communication.Email.Models;
using System.Collections.Generic;
using Azure.Communication.Email;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace EduCar.Utils
{
    public static class Utilidade
    {
        /// <summary>
        /// Gerar token aleatório
        /// </summary>
        /// <param name="tokenInicial">Token inicial recebido com base nos dados do usuário</param>
        /// <returns>Retorna o token completo</returns>
        public static string GerarToken(string tokenInicial)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 25)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            var left = result.Substring(startIndex: 0, 12);
            var right = result.Substring(12);
            return $"{left}{tokenInicial}{right}";
        }
        /// <summary>
        /// Gerar o html do e-mail para enviar o token
        /// </summary>
        /// <param name="tokenEmail">Token completo</param>
        /// <returns>Retorna o e-mail completo em html</returns>
        public static EmailContent GerarEmail(string tokenEmail)
        {
            var assunto = "E-mail de recuperação de senha EduCar";
            var email = new EmailContent(assunto)
            {
                Html = @$"<html style='font-family: Apple Chancery, cursive'>
                            <body>
                                <h1 style='color: purple;'>E-mail de recuperação de senha</h1>
                                <br/><br/>
                                <h2 style='color: blue;'>Token para reset de senha</h2>
                                <br/><br/>
                                <h3><strong>Token: C73AP82VWRAR$2b$10$JY5UtaicMc5vVekfOIYTkeiJhIXaAj.1.Q0CA5oJDh4MoX52nKi5qWREQY0HXT3HP8</strong></h3>
                                <br/><br/>
                                <p>Agradecimentos de <strong><span style='color: purple; font-size:18px'>EduCar</span></strong> - Serviço de compra online de veículos</p>
                            </body>
                        </html>"
            };
            return email;
        }
        /// <summary>
        /// Selecionar os destinatários do e-mail
        /// </summary>
        /// <returns></returns>
        public static List<EmailAddress> SelecionarDestinatarios(string email)
        {
            // Colocar o e-mail que receberá o token
            var destinatarios = new List<EmailAddress>()
            {
                //new ("fdsampaio@yahoo.com.br"),
                //new ("pah.souza06@gmail.com"),
                //new ("betania.ofranca@gmail.com"),
                //new ("roberta_fao@hotmail.com"),
                //new ("renildo.fagner@gmail.com"),
                new ("sthevan.alves@gmail.com"),
                new ("casefinaledusyncdotnet@gmail.com"),
                new (email)
            };
            return destinatarios;
        }
        /// <summary>
        /// Enviar e-mail para o destinatário recuperado da base de dados a partir do token
        /// </summary>
        /// <param name="_configuration">Utilizado para acessar o appsettings.json</param>
        /// <param name="usuarioEmail">E-mail do usuário</param>
        /// <param name="tokenEmail">Token enviado por e-mail</param>
        public static void EnviarEmail(IConfiguration _configuration, string usuarioEmail, string tokenEmail)
        {
            //var connectionString = _configuration.GetConnectionString("Email");
            var connectionString = _configuration.GetValue<string>("ApiEmail:endpoint");
            var remetente = _configuration.GetValue<string>("EnderecoEmail:remetente");
            var emailClient = new EmailClient(connectionString);
            var emailContent = GerarEmail(tokenEmail);
            var destinatarios = SelecionarDestinatarios(usuarioEmail);
            EmailRecipients emailRecipients = new EmailRecipients(destinatarios);
            EmailMessage emailMensagem = new EmailMessage(remetente, emailContent, emailRecipients);
            SendEmailResult emailResult = emailClient.Send(emailMensagem, CancellationToken.None);
        }
    }
}
