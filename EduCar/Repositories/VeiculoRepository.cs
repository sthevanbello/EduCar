using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Veiculo
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class VeiculoRepository : BaseRepository<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(EduCarContext context) : base(context)
        {
        }
    }
}
