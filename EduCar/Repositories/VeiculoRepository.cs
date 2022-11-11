using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Veiculo
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class VeiculoRepository : BaseRepository<Veiculo>, IVeiculoRepository
    {
        private readonly EduCarContext _context;
        public VeiculoRepository(EduCarContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Exibe uma lista de placas dos veículos selecionados
        /// <returns>Retorna uma lista de placas relacionadas com os veículos</returns>
        /// </summary>
        public Veiculo GetPlaca(string placa)
        {
            var veiculoPlaca = _context.Veiculo

                  .Include(s => s.StatusVenda)  // inclui a classe StatusVenda para ser exibida
                  .Include(con => con.Concessionaria)  // inclui a classe Concessionaria para ser exibida
                  .Include(f => f.FichaTecnica)  // inclui a classe FichaTecnica para ser exibida
                  .Include(c => c.CaracteristicasGerais) // inclui a classe CaracteristicasGerais para ser exibida
                  .FirstOrDefault(c => c.CaracteristicasGerais.Placa == placa); // busca a placa selecionada do veículo correspondente
            return veiculoPlaca;
        }


        /// <summary>
        /// Exibe uma lista de veículos com o status 'Disponível'
        /// <returns>Retorna uma lista de veículos disponíveis</returns>
        /// </summary>
        public ICollection<Veiculo> GetStatusDisponivel()
        {
            var veiculos = _context.Veiculo
                .Include(s => s.StatusVenda)
                .Include(con => con.Concessionaria)
                .Include(f => f.FichaTecnica)
                .Include(c => c.CaracteristicasGerais)
                .Where(s => s.StatusVenda.Status == "Disponível")
                .ToList();

            return veiculos;

        }

        /// <summary>
        /// Deleta todas as dependências quando um veículo for deletado
        /// As foreign keys são também deletadas quando um veículo for excluído
        /// </summary>
        public void DeleteAllDependencies(Veiculo veiculo)
        {
            _context.Veiculo.Remove(veiculo);

            var fichaTecnica = veiculo.FichaTecnica;
            _context.FichaTecnica.Remove(fichaTecnica);

            var caracteristicasGerais = veiculo.CaracteristicasGerais;
            _context.CaracteristicasGerais.Remove(caracteristicasGerais);

            _context.SaveChanges();
        }

        /// <summary>
        ///Exibe todas as informações do veículo informado
        ///<returns>Retorna uma lista de todos os detalhes do veículo selecionado</returns>
        /// </summary>
        public ICollection<Veiculo> GetAllVeiculosCompletos()
        {
            var veiculoCompleto = _context.Veiculo
                                .Include(f => f.FichaTecnica)
                                .ThenInclude(d => d.Direcao)
                                .Include(c => c.FichaTecnica.Cambio)
                                .Include(c => c.CaracteristicasGerais)
                                .Include(con => con.Concessionaria)
                                .ThenInclude(e => e.Endereco)
                                .Include(s => s.StatusVenda)
                                .ToList();
                return veiculoCompleto;
        }
    }
}
