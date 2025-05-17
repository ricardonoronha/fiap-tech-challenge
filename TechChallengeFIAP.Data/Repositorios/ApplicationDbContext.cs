using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Data.Repositorios
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _conectionString;

        public ApplicationDbContext(string conectionString)
        {
            _conectionString = conectionString;
        }

        public DbSet<Jogo> Jogo{ get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Promocao> Promocao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_conectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogo>(e =>
            {
                e.ToTable("Jogo");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
                e.Property(p => p.NomeJogo).HasColumnType("VARCHAR(100)");
                e.Property(p => p.DescricaoJogo).HasColumnType("VARCHAR(100)");
                e.Property(p => p.ClassificacaoJogo).HasColumnType("VARCHAR(100)");
                e.Property(p => p.DataLancamento).HasColumnType("DATETIME");
                e.Property(p => p.ValorBase).HasColumnType("DECIMAL(10,2)");
                e.Property(p => p.ValorPromocao).HasColumnType("DECIMAL(10,2)");
                e.Property(p => p.EhInativo).HasColumnType("BIT");
            });

            modelBuilder.Entity<Pessoa>(e =>
            {
                e.ToTable("Pessoa");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
                e.Property(p => p.NomeCompleto).HasColumnType("VARCHAR(100)").UseIdentityColumn();
                e.Property(p => p.NomeUsuario).HasColumnType("VARCHAR(100)").UseIdentityColumn();
                e.Property(p => p.EmailUsuario).HasColumnType("VARCHAR(100)").UseIdentityColumn();
                e.Property(p => p.DataNascimento).HasColumnType("DATETIME").UseIdentityColumn();
                e.Property(p => p.HashSenha).HasColumnType("VARCHAR(100)").UseIdentityColumn();
                e.Property(p => p.EhAdministrador).HasColumnType("BIT").UseIdentityColumn();
                e.Property(p => p.EhAtivo).HasColumnType("BIT").UseIdentityColumn();
                e.Property(p => p.DataCriacao).HasColumnType("DATETIME").UseIdentityColumn();
                e.Property(p => p.UsuarioCriador).HasColumnType("VARCHAR(100)").UseIdentityColumn();
                e.Property(p => p.DataAtualizacao).HasColumnType("DATETIME").UseIdentityColumn().IsRequired();
                e.Property(p => p.UsuarioAtualizador).HasColumnType("VARCHAR(100)").UseIdentityColumn();
                
            });

            modelBuilder.Entity<Promocao>(e =>
            {
                e.ToTable("Promocao");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
                e.Property(p => p.DataInicio).HasColumnType("DATETIME").UseIdentityColumn();
                e.Property(p => p.DataFim).HasColumnType("DATETIME").UseIdentityColumn();
                e.Property(p => p.JogoId).HasColumnType("INT").UseIdentityColumn();
                e.Property(p => p.EhCancelada).HasColumnType("BIT").UseIdentityColumn();

            });
        }
    }
}
