using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Usuario
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(EduCarContext context) : base(context)
        {
        }
    }
}
