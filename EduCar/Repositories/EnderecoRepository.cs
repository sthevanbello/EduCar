using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Models;

namespace EduCar.Repositories
{
    /// <summary>
    /// Repositório da Model Endereco
    /// Todos os métodos básicos estão na BaseRepository
    /// </summary>
    public class EnderecoRepository : BaseRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(EduCarContext context) : base(context)
        {
        }
    }
}
