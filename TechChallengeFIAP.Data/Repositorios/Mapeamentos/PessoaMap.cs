using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Data.Repositorios.Mapeamentos
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.NomeCompleto).HasColumnType("VARCHAR(100)");
            builder.Property(p => p.NomeUsuario).HasColumnType("VARCHAR(100)");
            builder.Property(p => p.EmailUsuario).HasColumnType("VARCHAR(100)");
            builder.Property(p => p.DataNascimento).HasColumnType("DATETIME");
            builder.Property(p => p.HashSenha).HasColumnType("VARCHAR(100)");
            builder.Property(p => p.EhAdministrador).HasColumnType("BIT");
            builder.Property(p => p.EhAtivo).HasColumnType("BIT");
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME");
            builder.Property(p => p.UsuarioCriador).HasColumnType("VARCHAR(100)");
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME");
            builder.Property(p => p.UsuarioAtualizador).HasColumnType("VARCHAR(100)");
        }
    }
}
