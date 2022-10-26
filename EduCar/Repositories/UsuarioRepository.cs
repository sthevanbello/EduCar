using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Usuario
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly EduCarContext _context;

        public UsuarioRepository(EduCarContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// Retornar um usuário a partir do e-mail fornecido
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <returns>Retorna um usuário a partir do e-mail fornecido</returns>
        public Usuario GetByEmailUsuario(string email)
        {
            var usuario = _context.Usuario
                            .Include(e => e.Endereco)
                            .Include(c => c.Cartoes)
                            .Include(x => x.Pedidos)
                                .ThenInclude(v => v.Veiculo)
                                .ThenInclude(c => c.Concessionaria)
                            .FirstOrDefault(u => u.Email == email);
            return usuario;
        }
    }
}
