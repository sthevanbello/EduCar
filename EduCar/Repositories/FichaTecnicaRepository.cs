using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model FichaTecnica
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class FichaTecnicaRepository : BaseRepository<FichaTecnica>, IFichaTecnicaRepository
    {
        public FichaTecnicaRepository(EduCarContext context) : base(context)
        {
        }
    }
}
