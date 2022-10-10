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
                  .Include(c => c.CaracteristicasGerais) // inclui a classe CaracteristicasGerais para ser exibida
                  .Include(f => f.FichaTecnica)  // inclui a classe FichaTecnica para ser exibida
                  .FirstOrDefault(c => c.CaracteristicasGerais.Placa == placa); // busca a placa selecionada do veículo correspondente         

            return veiculoPlaca;
        }

        public ICollection<Veiculo> GetStatusDisponivel()
        {
            var veiculos = _context.Veiculo
                .Include(s => s.StatusVenda)
                .Where(s => s.StatusVenda.Status == "Disponivel")
                .ToList();

            return veiculos;

        }
    }
}
