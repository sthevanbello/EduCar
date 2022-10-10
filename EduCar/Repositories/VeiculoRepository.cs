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
                  .Include(c => c.CaracteristicasGerais) // inclui a classe Medico para ser exibida
                   .Include(f => f.FichaTecnica)  // inclui a classe Especialidade para ser exibida
                  .FirstOrDefault(v => v.CaracteristicasGerais.Placa == placa); // quando o id do tipo de usuario for igual a 1 (que são os médicos)           

            return veiculoPlaca;
        }

        public ICollection<Veiculo> GetStatusDisponivel()
        {
            throw new System.NotImplementedException();
        }
    }
}
