using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Endereco
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class EnderecoRepository : BaseRepository<Endereco>, IEnderecoRepository
    {
        private readonly EduCarContext _context;
        public EnderecoRepository(EduCarContext context) : base(context)
        {
            _context = context;
        }

        public ICollection<Concessionaria> GetAllEnderecosConcessionarias()
        {
            var enderecosConcessionarias = _context.Concessionaria
                                            .Include(x => x.Endereco)
                                            .ToList();
            return enderecosConcessionarias;
                                            
        }

        public Usuario GetByEmailEnderecoUsuario(string email)
        {
             var enderecoUsuario = _context.Usuario
                .Include(u => u.Endereco)
                .FirstOrDefault(u => u.Email == email);
            return enderecoUsuario;
        }
    }
}
