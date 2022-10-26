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

        /// <summary>
        ///Verifica o status de venda do pedido após ele ser criado, ele se torna indisponível
        ///<returns>Retorna o pedido criado e com status indisponível</returns>
        /// </summary>
        public Pedido InsertPedidoVenda(Pedido pedido)
        {
            var veiculo = _context.Veiculo
                .FirstOrDefault(v => v.Id == pedido.IdVeiculo);

            if (veiculo.IdStatusVenda == 2)
            {
                return null;
            }

            veiculo.IdStatusVenda = 2;

            _context.Veiculo.Update(veiculo);

            var retorno = _context.Pedido.Add(pedido);

            _context.SaveChanges();

            return retorno.Entity;
        }

        /// <summary>
        ///Exibe as informações do usuário do pedido informado
        ///<returns>Retorna uma lista das informações do usuário do pedido selecionado</returns>
        /// </summary>
        public ICollection<Pedido> GetPedidosByUsuario(string email)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Email == email);

            if (usuario is null)
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
            if (pedidos.Count>0)
            {
                pedidos.ForEach(c => c.Cartao = new Cartao
                {
                    Titular = c.Cartao.Titular
                });
            }
            return pedidos;
        }

        /// <summary>
        ///Exibe a concessionária do pedido informado
        ///<returns>Retorna uma lista de pedidos com as concessionarias incluídas</returns>
        ///</summary>
        public ICollection<Pedido> GetPedidosByConcessionaria(int id)
        {
            var concessionaria = _context.Concessionaria.FirstOrDefault(i => i.Id == id);

            if (concessionaria is null)
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

        ///<summary>
        ///Exibe todas as informações do veículo informado
        ///<returns>Retorna uma lista de pedidos com suas respectivas concessionárias</returns>
        /// </summary>
        public ICollection<Pedido> GetPedidosCompletos()
        {
            var pedidos = _context.Pedido
                  .Include(u => u.Usuario)
                  .ThenInclude(t => t.TipoUsuario)
                  .Include(c => c.Concessionaria)
                  .Include(v => v.Veiculo)
                  .ToList();
            return pedidos;
        }
    }
}
