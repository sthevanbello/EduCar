using System.Linq;
using System;
using Azure.Communication.Email.Models;
using System.Collections.Generic;

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
                Html = @$"<html>
                            <body>
                                <h1>E-mail de recuperação de senha</h1>
                                <br/>
                                <h2>Token para reset de senha</h2>
                                <h3><strong>Token: {tokenEmail}</strong></h3>
                                <br/>
                                <p>Agradecimentos de <strong>EduCar</strong> - Serviço de compra online de veículos</p>
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
    }
}
