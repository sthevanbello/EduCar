using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;
using System.Collections.Generic;
using System.Linq;

namespace EduCar.Repositories
{
    public class CaracteristicasGeraisRepository : BaseRepository<Pedido>, ICaracteristicasGeraisRepository
    {
        public CaracteristicasGeraisRepository(EduCarContext context) : base(context)
        {
        }

    }
}
