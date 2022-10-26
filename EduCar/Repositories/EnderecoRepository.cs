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
        /// <summary>
        /// Retornar as concessionárias com endereço completo
        /// </summary>
        /// <returns>Retorna as concessionárias com endereço completo</returns>
        public ICollection<Concessionaria> GetAllEnderecosConcessionarias()
        {
            var enderecosConcessionarias = _context.Concessionaria
                                            .Include(x => x.Endereco)
                                            .ToList();
            return enderecosConcessionarias;
                                            
        }
        /// <summary>
        /// Retornar um usuário com endereço completo a partir do e-mail fornecido
        /// </summary>
        /// <param name="email">E-mail do usuario</param>
        /// <returns>Retornar um usuário com endereço completo a partir do e-mail fornecido</returns>
        public Usuario GetByEmailEnderecoUsuario(string email)
        {
             var enderecoUsuario = _context.Usuario
                .Include(u => u.Endereco)
                .FirstOrDefault(u => u.Email == email);
            return enderecoUsuario;
        }
    }
}
