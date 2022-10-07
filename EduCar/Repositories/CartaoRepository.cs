using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Cartao
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class CartaoRepository : BaseRepository<Cartao>, ICartaoRepository
    {
        public CartaoRepository(EduCarContext context) : base(context)
        {
        }
    }
}
