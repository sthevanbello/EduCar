using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    public class CambioRepository : BaseRepository<Cambio>, ICambioRepository
    {
        public CambioRepository(EduCarContext context) : base(context)
        {
        }
    }
}
