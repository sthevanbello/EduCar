using EduCar.Controllers;
using EduCar.Interfaces;
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
    public class EnderecosControllerTests
    {
        // Preparação  
        private readonly Mock<IEnderecoRepository> _mock; // A função Mock cria um repositório "fake" para usá-lo no controller
        private readonly EnderecosController _ctl;

        public EnderecosControllerTests()  // Gerando um método construtor
        {
            _mock = new Mock<IEnderecoRepository>();
            _ctl = new EnderecosController(_mock.Object);
        }
        /// <summary>
        /// Esse teste precisa retornar o método com um resultado = Ok
        /// </summary>
        [Fact]  // Determina o que é um teste
        public void TestActionResultGetReturnOk()
        {

            // Execução
            var result = _ctl.GetAllEnderecos(); // Ele busca o método GET criado no Controller verdadeiro
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
            var actionResult = _ctl.GetAllEnderecos();
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
            var actionResult = _ctl.GetAllEnderecos();
            // Retorno
            Assert.NotNull(actionResult);
        }
        
    }
}
