using EduCar.Models;
using System.Collections.Generic;

namespace EduCar.Interfaces
{
    /// <summary>
    /// Interface de CartaoRepository
    /// </summary>
    public interface ICartaoRepository : IBaseRepository<Cartao>
    {
        // Todos os métodos básicos estão a IBaseRepository
        ICollection<Cartao> GetByEmailCartoes(string email);
    }
}
