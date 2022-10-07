using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Direcao
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class DirecaoRepository : BaseRepository<Direcao>, IDirecaoRepository
    {
        public DirecaoRepository(EduCarContext context) : base(context)
        {
        }
    }
}
