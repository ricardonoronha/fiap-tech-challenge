using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Data.Repositorios
{
    public class ApplicationDbContext : DbContext
    {

        // DbContextOptions<SgtWebDb> options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public ApplicationDbContext()
        {
        }

        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Promocao> Promocao { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configuração que permite o mapeamento de todas as classes que herdam IEntityTypeConfiguration. 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
