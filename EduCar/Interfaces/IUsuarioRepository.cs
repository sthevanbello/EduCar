using EduCar.Models;

namespace EduCar.Interfaces
{
    /// <summary>
    /// Interface de UsuarioRepository
    /// </summary>
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        // Todos os métodos básicos estão a IBaseRepository
        public Usuario GetByEmailUsuario(string email);
    }
}
