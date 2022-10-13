using EduCar.Models;
using System.Collections.Generic;

namespace EduCar.Interfaces
{
    /// <summary>
    /// Interface de PedidoRepository
    /// </summary>
    public interface IPedidoRepository : IBaseRepository<Pedido>
    {
        // Todos os métodos básicos estão a IBaseRepository
        public Pedido InsertPedidoVenda(Pedido pedido);

        public ICollection<Pedido> GetPedidosByUsuario(string email);

        public ICollection<Pedido> GetPedidosByConcessionaria(int id);
        public ICollection<Pedido> GetPedidosCompletos();
    }
}
