using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Concessionaria
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class ConcessionariaRepository : BaseRepository<Concessionaria>, IConcessionariaRepository
    {
        public ConcessionariaRepository(EduCarContext context) : base(context)
        {
        }
    }
}
