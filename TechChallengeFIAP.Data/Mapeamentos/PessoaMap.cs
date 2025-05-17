using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Data.Mapeamentos
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.NomeCompleto).HasColumnType("VARCHAR(100)").UseIdentityColumn();
            builder.Property(p => p.NomeUsuario).HasColumnType("VARCHAR(100)").UseIdentityColumn();
            builder.Property(p => p.EmailUsuario).HasColumnType("VARCHAR(100)").UseIdentityColumn();
            builder.Property(p => p.DataNascimento).HasColumnType("DATETIME").UseIdentityColumn();
            builder.Property(p => p.HashSenha).HasColumnType("VARCHAR(100)").UseIdentityColumn();
            builder.Property(p => p.EhAdministrador).HasColumnType("BIT").UseIdentityColumn();
            builder.Property(p => p.EhAtivo).HasColumnType("BIT").UseIdentityColumn();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").UseIdentityColumn();
            builder.Property(p => p.UsuarioCriador).HasColumnType("VARCHAR(100)").UseIdentityColumn();
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME").UseIdentityColumn().IsRequired();
            builder.Property(p => p.UsuarioAtualizador).HasColumnType("VARCHAR(100)").UseIdentityColumn();
        }
    }
}
