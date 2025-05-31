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
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<RegistroEvento> RegistroEventos { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder
                .Entity<Pessoa>()
                .HasData(new Pessoa()
                {
                    Id = new Guid("9f4ab7ce-9c51-42b2-86bd-701c9f61ddca"),
                    NomeCompleto = "admin",
                    NomeUsuario = "admin",
                    EmailUsuario = "admin@dominio-exemplo.com",
                    DataCriacao = new DateTime(2025, 5, 24, 10, 15, 0),
                    DataNascimento = new DateTime(1970, 1, 1, 0, 0, 0),
                    EhAdministrador = true,
                    EhAtivo = true,
                    HashSenha = "oTa8GZ7m5lROX1ZZyVlznSTim7oiF8ycUtd++6u0fycaD7jesnyvrNNpzy/A9Pkl",
                });


            
            //Configuração que permite o mapeamento de todas as classes que herdam IEntityTypeConfiguration. 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            
        }
    }
}
