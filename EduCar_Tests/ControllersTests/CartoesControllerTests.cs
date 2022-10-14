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
    public class CartoesControllerTests
    {
        // Preparação  
        private readonly Mock<ICartaoRepository> _mock; // A função Mock cria um repositório "fake" para usá-lo no controller
        private readonly CartoesController _ctl;

        public CartoesControllerTests()  // Gerando um método construtor
        {
            _mock = new Mock<ICartaoRepository>();
            _ctl = new CartoesController(_mock.Object);
        }
        /// <summary>
        /// Esse teste precisa retornar o método com um resultado = Ok
        /// </summary>
        [Fact]  // Determina o que é um teste
        public void TestActionResultGetReturnOk()
        {

            // Execução
            var result = _ctl.GetAllCartoes(); // Ele busca o método GET criado no Controller verdadeiro
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
            var actionResult = _ctl.GetAllCartoes();
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
            var actionResult = _ctl.GetAllCartoes();
            // Retorno
            Assert.NotNull(actionResult);
        }
    }
}
