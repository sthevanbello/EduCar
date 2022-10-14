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
    public class VeiculosControllerTests
    {
        // Preparação  
        private readonly Mock<IVeiculoRepository> _mock; // A função Mock cria um repositório "fake" para usá-lo no controller
        private readonly VeiculosController _ctl;

        public VeiculosControllerTests()  // Gerando um método construtor
        {
            _mock = new Mock<IVeiculoRepository>();
            _ctl = new VeiculosController(_mock.Object);
        }
        /// <summary>
        /// Esse teste precisa retornar o método com um resultado de Ok
        /// </summary>
        [Fact]  // Determina o que é um teste
        public void TestActionResultReturnOkBuscas()
        {

            // Execução
            var result = _ctl.GetAllVeiculos(); // Ele busca o método GET criado no Controller verdadeiro
            // Retorno
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        /// Esse teste precisa retornar o método, mas com o status de code de sucesso = 200
        /// </summary>
        [Fact]
        public void TestStatusCodeSuccessBuscas()
        {
            // Execução - Act
            var actionResult = _ctl.GetAllVeiculos();
            var result = actionResult as OkObjectResult;
            // Retorno
            Assert.Equal(200, result.StatusCode); // Status Code 200
        }
        /// <summary>
        /// Esse teste precisa retornar o método com um resultado de Ok
        /// </summary>
        [Fact]
        public void TestInsert()
        {
            var result = _ctl.InsertVeiculo(new() // Método POST do Controller verdadeiro
            {
                Nome = "TesteDrive",
                Valor = 3000,
                IdStatusVenda = 1,
                IdConcessionaria = 1,
                IdFichaTecnica = 1,
                IdCaracteristicasGerais = 1,

            });
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        /// Esse teste precisa retornar um resultado não nulo
        /// </summary>
        [Fact]
        public void TestActionResultBuscaNotNull()
        {
            // Execução 
            var actionResult = _ctl.GetAllVeiculos();
            // Retorno
            Assert.NotNull(actionResult);
        }
    }
}
