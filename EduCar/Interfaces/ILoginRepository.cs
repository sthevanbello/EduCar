using EduCar.Models;

namespace EduCar.Interfaces
{
    /// <summary>
    /// Interface de LoginRepository
    /// </summary>
    public interface ILoginRepository
    {
        public string Logar(Login login);
    }
}
