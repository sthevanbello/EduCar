using EduCar.Models;

namespace EduCar.Interfaces
{
    /// <summary>
    /// Interface de LoginRepository
    /// </summary>
    public interface ILoginRepository
    {
        public string Logar(Login login);
        public object SolicitarTokenSenha(string email);
        public object TrocarSenha(string token, string senhaNova, string cpf);
    }
}
