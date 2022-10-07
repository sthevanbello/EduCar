using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    public class FichaTecnicaRepository : BaseRepository<FichaTecnica>, IFichaTecnicaRepository
    {
        public FichaTecnicaRepository(EduCarContext context) : base(context)
        {
        }
    }
}
