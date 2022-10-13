using EduCar.Controllers;
using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EduCar_Tests.ControllersTests
{
    public class UsuariosControllerTests
    {
        // Preparação  
        private readonly Mock<IUsuarioRepository> _mock; // A função Mock cria um repositório "fake" para usá-lo no controller
        private readonly UsuariosController _ctl;

        public UsuariosControllerTests()  // Gerando um método construtor
        {
            _mock = new Mock<IUsuarioRepository>();
            _ctl = new UsuariosController(_mock.Object);
        }
        /// <summary>
        /// Esse teste precisa retornar o método com um resultado = Ok
        /// </summary>
        [Fact]  // Determina o que é um teste
        public void TestActionResultGetReturnOk()
        {

            // Execução
            var result = _ctl.GetAllUsuarios(); // Ele busca o método GET criado no Controller verdadeiro
            // Retorno
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        /// Esse teste precisa retornar o método, mas com o status de code de sucesso = 200
        /// </summary>
        [Fact]
        public void TestStatusCodeGetSuccess()
        {
            // Execução - Act
            var actionResult = _ctl.GetAllUsuarios();
            var result = actionResult as OkObjectResult;
            // Retorno
            Assert.Equal(200, result.StatusCode); // Status Code 200
        }

        /// <summary>
        /// Esse teste precisa retornar um resultado não nulo
        /// </summary>
        [Fact]
        public void TestActionResultBuscaNotNull()
        {
            // Execução 
            var actionResult = _ctl.GetAllUsuarios();
            // Retorno
            Assert.NotNull(actionResult);
        }

        /// <summary>
        /// Teste de insert da entidade atual
        /// </summary>
        [Fact]
        public void TestInsertCliente()
        {
            var result = _ctl.InsertUsuarioCliente(new()
            {// Criação de um objeto para um teste de inserção
                Nome = "Teste",
                Sobrenome = "Teste",
                CPF_CNPJ = "12345678900",
                Celular = "12345678900",
                Email = "teste@email.com",
                Senha = "123456789",
                Aceite = true,
                IdEndereco = 1,
                IdTipoUsuario = 1,
            });
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Teste de insert da entidade atual
        /// </summary>
        [Fact]
        public void TestInsertVendedor()
        {
            var result = _ctl.InsertUsuarioVendedor(new()
            {
                Nome = "Teste",
                Sobrenome = "Teste",
                CPF_CNPJ = "12345678900",
                Celular = "12345678900",
                Email = "teste@email.com",
                Senha = "123456789",
                Aceite = true,
                IdEndereco = 1,
                IdTipoUsuario = 2,
            });
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Teste de insert da entidade atual
        /// </summary>
        [Fact]
        public void TestInsertAdmin()
        {
            var result = _ctl.InsertUsuarioAdministrador(new()
            {
                Nome = "Teste",
                Sobrenome = "Teste",
                CPF_CNPJ = "12345678900",
                Celular = "12345678900",
                Email = "teste@email.com",
                Senha = "123456789",
                Aceite = true,
                IdEndereco = 1,
                IdTipoUsuario = 3,
            });
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
