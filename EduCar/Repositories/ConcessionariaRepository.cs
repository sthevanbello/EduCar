using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Concessionaria
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class ConcessionariaRepository : BaseRepository<Concessionaria>, IConcessionariaRepository
    {
        private readonly EduCarContext _context;
        public ConcessionariaRepository(EduCarContext context) : base(context)
        {
            _context = context;
        }

        public ICollection<Concessionaria> GetAllConcessionariaComEndereco()
        {
            var concessionariaComEndereco = _context.Concessionaria
                                            .Include(e => e.Endereco)
                                            .ToList();
            return concessionariaComEndereco;
        }
    }
}
