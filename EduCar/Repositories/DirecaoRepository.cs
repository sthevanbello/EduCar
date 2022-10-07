using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    public class DirecaoRepository : BaseRepository<Direcao>, IDirecaoRepository
    {
        public DirecaoRepository(EduCarContext context) : base(context)
        {
        }
    }
}
