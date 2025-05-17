using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Data.Repositorios.Mapeamentos
{
    public class PromocaoMap : IEntityTypeConfiguration<Promocao>
    {
        public void Configure(EntityTypeBuilder<Promocao> builder)
        {
           builder.ToTable("Promocao");
           builder.HasKey(p => p.Id);
           builder.Property(p => p.Id).HasColumnType("INT");
           builder.Property(p => p.DataInicio).HasColumnType("DATETIME");
           builder.Property(p => p.DataFim).HasColumnType("DATETIME");
           builder.Property(p => p.JogoId).HasColumnType("INT");
           builder.Property(p => p.EhCancelada).HasColumnType("BIT");
        }
    }
}
