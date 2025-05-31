using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Data.Repositorios.Mapeamentos
{
    public class JogoMap : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.ToTable("Jogo");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.NomeJogo).HasColumnType("VARCHAR(100)");
            builder.Property(p => p.DescricaoJogo).HasColumnType("VARCHAR(100)");
            builder.Property(p => p.ClassificacaoJogo).HasColumnType("VARCHAR(100)");
            builder.Property(p => p.DataLancamento).HasColumnType("DATETIME2");
            builder.Property(p => p.ValorBase).HasColumnType("DECIMAL(10,2)");
            builder.Property(p => p.ValorPromocao).HasColumnType("DECIMAL(10,2)");
            builder.HasMany(e => e.Promocoes).WithOne(e => e.Jogo).HasForeignKey(c => c.JogoId).IsRequired();
        }
    }
}
