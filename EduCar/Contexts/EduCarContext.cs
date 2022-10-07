using EduCar.Models;
using Microsoft.EntityFrameworkCore;

namespace EduCar.Contexts
{
    public class EduCarContext : DbContext
    {
        public EduCarContext(DbContextOptions<EduCarContext> options) : base(options)
        {

        }

        public DbSet<Cambio> Cambio { get; set; }
        public DbSet<CaracteristicasGerais> CaracteristicasGerais { get; set; }
        public DbSet<Cartao> Cartao { get; set; }
        public DbSet<Concessionaria> Concessionaria { get; set; }
        public DbSet<Direcao> Direcao { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<FichaTecnica> FichaTecnica { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Veiculo> Veiculo { get; set; }

    }
}
