using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EduCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {             
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculosController(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        /// <summary>
        /// Inserir um veículo no banco.
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="veiculo">Veículo a ser inserido</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um veículo inserido ou uma mensagem se houve alguma falha</returns>
        [HttpPost]
        public IActionResult InsertVeiculo(Veiculo veiculo)
        {
            try
            {
                var veiculoInserido = _veiculoRepository.Insert(veiculo);
                return Ok(veiculoInserido);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = "Falha ao inserir um veículo no banco",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir uma lista de veículos cadastrados no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de Veículos</returns>
        [HttpGet]
        public IActionResult GetAllVeiculos()
        {
            try
            {
                var veiculos = _veiculoRepository.GetAll();
                return Ok(veiculos);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os veículos",
                    ex.InnerException.Message
                });
            }
        }

        /// <summary>
        /// Exibir um veículo a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id do veículo</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna um Veículo</returns>
        [HttpGet("{id}")]
        public IActionResult GetByIdVeiculo(int id)
        {
            try
            {
                var veiculo = _veiculoRepository.GetById(id);
                if (veiculo is null)
                {
                    return NotFound(new { msg = "Veículo não foi encontrado. Verifique se o Id está correto" });
                }
                return Ok(veiculo);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao exibir o veículo",
                    ex.InnerException.Message
                });
            }
        }


        /// <summary>
        /// Exibir uma lista das placas dos veículos cadastrados no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de placas</returns>
        [HttpGet("Placa")]
        public IActionResult GetLicensePlates(string placa)
        {
            try
            {
                var placaVeiculo = _veiculoRepository.GetPlaca(placa);

                if (placaVeiculo is null)
                {
                    return BadRequest(new
                    {
                        msg = "Não foi possível encontrar uma placa"
                    });
                }

                return Ok(placaVeiculo);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar as placas dos veículos",
                    ex.InnerException.Message
                });
            }
        }

        // <summary>
        /// Exibir uma lista de veículos apenas disponíveis cadastrados no sistema
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma lista de veículos disponíveis</returns>
        [HttpGet("Status")]
        public IActionResult GetAvailableVehicles()
        {
            try
            {
                var veiculoDisponivel = _veiculoRepository.GetStatusDisponivel();
                return Ok(veiculoDisponivel);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao os veículos disponíveis",
                    ex.InnerException.Message
                });
            }
        }


        /// <summary>
        /// Alterar um veículo a partir do Id fornecido
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id do veículo</param>
        /// <param name="veiculo">Dados atualizados</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem dizendo se o veículo foi alterado ou se houve algum erro</returns>
        [HttpPut("{id}")]
        public IActionResult PutVeiculo(int id, Veiculo veiculo)
        {
            try
            {
                if (id != veiculo.Id)
                {
                    return BadRequest(new { msg = "Os ids não são correspondentes" });
                }
                var veiculoRetorno = _veiculoRepository.GetById(id);

                if (veiculoRetorno is null)
                {
                    return NotFound(new { msg = "Veículo não encontrado. Favor, conferir o Id informado" });
                }

                _veiculoRepository.Put(veiculo);

                return Ok(new { msg = "Veículo alterado", veiculo });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar o veículo",
                    ex.InnerException.Message
                });
            }
        }
        /// <summary>
        /// Excluir veículo do banco de dados
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        /// 
        /// 
        /// </remarks>
        /// <param name="id">Id do veículo a ser excluído</param>
        /// <response code="401">Acesso negado</response>
        /// <response code="403">Nível de acesso não está autorizado</response>
        /// <returns>Retorna uma mensagem informando se o veículo foi excluído ou se houve falha</returns>
        //[Authorize(Roles = "Master")]
        [HttpDelete("{id}")]
        public IActionResult DeleteVeiculo(int id)
        {
            try
            {
                var veiculoRetorno = _veiculoRepository.GetById(id);

                if (veiculoRetorno is null)
                {
                    return NotFound(new { msg = "Veículo não encontrado. Conferir o Id informado" });
                }

                _veiculoRepository.Delete(veiculoRetorno);

                return Ok(new { msg = "Veículo excluído com sucesso" });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao excluir o veículo. Verifique se há utilização como Foreign Key.",
                    ex.InnerException.Message
                });
            }
        }
    }
}
