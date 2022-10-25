using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Cartao
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class CartaoRepository : BaseRepository<Cartao>, ICartaoRepository
    {
        private readonly EduCarContext _context;

        public CartaoRepository(EduCarContext context) : base(context)
        {
            _context = context;
        }

        public ICollection<Cartao> GetByEmailCartoes(string email)
        {
            var cartoes = _context.Cartao
                           .Include(u => u.Usuario)
                           .Where(t => t.Usuario.Email == email)
                           .ToList();
            return cartoes;
        }
    }
}
