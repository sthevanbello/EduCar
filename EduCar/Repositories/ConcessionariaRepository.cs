using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    public class ConcessionariaRepository : BaseRepository<Concessionaria>, IConcessionariaRepository
    {
        public ConcessionariaRepository(EduCarContext context) : base(context)
        {
        }
    }
}
