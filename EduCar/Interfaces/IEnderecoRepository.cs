using EduCar.Models;
using System.Collections.Generic;

namespace EduCar.Interfaces
{
    /// <summary>
    /// Interface de EnderecoRepository
    /// </summary>
    public interface IEnderecoRepository : IBaseRepository<Endereco>
    {
        // Todos os métodos básicos estão a IBaseRepository
        public ICollection<Concessionaria> GetAllEnderecosConcessionarias();
        public Usuario GetByEmailEnderecoUsuario(string email);
    }
}
