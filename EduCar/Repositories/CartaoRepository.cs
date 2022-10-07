using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    public class CartaoRepository : BaseRepository<Cartao>, ICartaoRepository
    {
        public CartaoRepository(EduCarContext context) : base(context)
        {
        }
    }
}
