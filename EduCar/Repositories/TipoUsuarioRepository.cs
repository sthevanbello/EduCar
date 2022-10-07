using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model TipoUsuario
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class TipoUsuarioRepository : BaseRepository<TipoUsuario>, ITipoUsuarioRepository
    {
        public TipoUsuarioRepository(EduCarContext context) : base(context)
        {
        }
    }
}
