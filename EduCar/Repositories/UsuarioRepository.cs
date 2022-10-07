using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(EduCarContext context) : base(context)
        {
        }
    }
}
