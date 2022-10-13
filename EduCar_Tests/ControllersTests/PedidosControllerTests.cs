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
    public class PedidosControllerTests
    {
        // Preparação  
        private readonly Mock<IPedidoRepository> _mock; // A função Mock cria um repositório "fake" para usá-lo no controller
        private readonly PedidosController _ctl;

        public PedidosControllerTests()  // Gerando um método construtor
        {
            _mock = new Mock<IPedidoRepository>();
            _ctl = new PedidosController(_mock.Object);
        }
        /// <summary>
        /// Esse teste precisa retornar o método com um resultado = Ok
        /// </summary>
        [Fact]  // Determina o que é um teste
        public void TestActionResultGetReturnOk()
        {

            // Execução
            var result = _ctl.GetAllPedidos(); // Ele busca o método GET criado no Controller verdadeiro
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
            var actionResult = _ctl.GetAllPedidos();
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
            var actionResult = _ctl.GetAllPedidos();
            // Retorno
            Assert.NotNull(actionResult);
        }

        /// <summary>
        /// Teste de insert da entidade atual
        /// </summary>
        [Fact]
        public void TestInsertSalvando()
        {
            var result = _ctl.InsertPedido(new()
            {// Criação de um objeto para um teste de inserção
                IdUsuario = 1,
                IdConcessionaria = 1,
                IdVeiculo = 1,
                IdCartao = 1,
                SalvarCartaoNoBanco = true
            });
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Teste de insert da entidade atual
        /// </summary>
        [Fact]
        public void TestInsertNaoSalvando()
        {
            var result = _ctl.InsertPedido(new()
            {// Criação de um objeto para um teste de inserção
                IdUsuario = 1,
                IdConcessionaria = 1,
                IdVeiculo = 1,
                IdCartao = 1,
                SalvarCartaoNoBanco = false
            });
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
