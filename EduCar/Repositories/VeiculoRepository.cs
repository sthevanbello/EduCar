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

        public ICollection<Veiculo> GetStatusDisponivel()
        {
            var veiculos = _context.Veiculo
                .Include(s => s.StatusVenda)
                .Include(con => con.Concessionaria)
                .Include(f => f.FichaTecnica)
                .Include(c => c.CaracteristicasGerais)
                .Where(s => s.StatusVenda.Status == "Disponivel")
                .ToList();

            return veiculos;

        }
    }
}
