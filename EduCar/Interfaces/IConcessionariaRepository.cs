using EduCar.Models;
using System.Collections.Generic;

namespace EduCar.Interfaces
{
    /// <summary>
    /// Interface de ConcessionariaRepository
    /// </summary>
    public interface IConcessionariaRepository : IBaseRepository<Concessionaria>
    {
        // Todos os métodos básicos estão a IBaseRepository
        public ICollection<Concessionaria> GetAllConcessionariaComEndereco();
    }
}
