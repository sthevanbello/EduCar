using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;
using System.Collections.Generic;
using System.Linq;

namespace EduCar.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(EduCarContext context) : base(context)
        {
        }

    }
}
