using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model StatusVenda
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class StatusVendaRepository : BaseRepository<StatusVenda>, IStatusVendaRepository
    {
        public StatusVendaRepository(EduCarContext context) : base(context)
        {
        }
    }
}
