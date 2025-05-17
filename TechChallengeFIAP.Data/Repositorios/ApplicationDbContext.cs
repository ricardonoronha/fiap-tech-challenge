using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Data.Mapeamentos;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Data.Repositorios
{
    public class ApplicationDbContext : DbContext
    {
        
        private readonly string _connectionString;
        // Construtor da classe ApplicationDbContext
        public ApplicationDbContext(string conectionString)
        {
            _connectionString = conectionString;
        }

        public DbSet<Jogo> Jogo{ get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Promocao> Promocao { get; set; }

        //definição configurações do banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Verificação se as opções do contexto já foram configuradas em outro lugar
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuração que permite o mapeamento de todas as classes que herdam IEntityTypeConfiguration. 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
