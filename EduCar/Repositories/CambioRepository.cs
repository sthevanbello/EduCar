using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Cambio
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class CambioRepository : BaseRepository<Cambio>, ICambioRepository
    {
        public CambioRepository(EduCarContext context) : base(context)
        {
        }
    }
}
