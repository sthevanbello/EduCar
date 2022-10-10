using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Pedido
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly EduCarContext _context;
        public PedidoRepository(EduCarContext context) : base(context)
        {
            _context = context;
        }

        public Pedido InsertPedidoVenda(Pedido pedido)
        {
            if (pedido.Veiculo.StatusVenda.Id == 2)
            {
                return null;
            }
            
            pedido.Veiculo.StatusVenda.Id = 2;

            var retorno = _context.Pedido.Add(pedido);

            _context.SaveChanges();

            return retorno.Entity;
        }

        public ICollection<Pedido> GetPedidosByUsuario(string email)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Email == email);

            if(usuario is null)
            {
                return null;
            }

            var pedidos = _context.Pedido
                  .Include(u => u.Usuario)
                  .Include(c => c.Concessionaria)
                  .Include(v => v.Veiculo)
                  .Include(c => c.Cartao)
                  .Where(e => e.Usuario.Email == email)
                  .ToList();

            pedidos.ForEach(c => c.Cartao = new Cartao
            {
                Titular = c.Cartao.Titular,
                Numero = "xxxx xxxx xxxx " + c.Cartao.Numero.Substring(12)
            });
                   
            return pedidos;
        }

        public ICollection<Pedido> GetPedidosByConcessionaria(int id)
        {
            var concessionaria = _context.Concessionaria.FirstOrDefault(i => i.Id == id);

            if(concessionaria is null)
            {
                return null;
            }

            var pedidos = _context.Pedido
                  .Include(u => u.Usuario)
                  .Include(c => c.Concessionaria)
                  .Include(v => v.Veiculo)
                  .Where(c => c.Concessionaria.Id == id)
                  .ToList();

            return pedidos;
        }

    }
}
