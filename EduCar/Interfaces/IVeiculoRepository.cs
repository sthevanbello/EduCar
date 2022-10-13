using EduCar.Models;
using System.Collections.Generic;

namespace EduCar.Interfaces
{
    /// <summary>
    /// Interface de VeiculoRepository
    /// </summary>
    public interface IVeiculoRepository : IBaseRepository<Veiculo>
    {
        // Todos os métodos básicos estão na IBaseRepository
        public Veiculo GetPlaca(string placa);
        ICollection<Veiculo> GetStatusDisponivel();
        ICollection<Veiculo> GetAllVeiculosCompletos();
        public void DeleteAllDependencies(Veiculo veiculo);
    }
}
