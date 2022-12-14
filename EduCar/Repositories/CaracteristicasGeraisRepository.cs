using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;
using System.Collections.Generic;
using System.Linq;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model CaracteristicasGerais
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class CaracteristicasGeraisRepository : BaseRepository<CaracteristicasGerais>, ICaracteristicasGeraisRepository
    {
        public CaracteristicasGeraisRepository(EduCarContext context) : base(context)
        {
        }

    }
}
