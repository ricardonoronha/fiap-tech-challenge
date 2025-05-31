using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFIAP.Domain.Entidades;

namespace TechChallengeFIAP.Data.Repositorios.Mapeamentos;

public class RegistroEventoMap : IEntityTypeConfiguration<RegistroEvento>
{
    public void Configure(EntityTypeBuilder<RegistroEvento> builder)
    {
        builder
            .ToTable(nameof(RegistroEvento));

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName(nameof(RegistroEvento.Id));

        builder
            .Property(x => x.Data)
            .HasColumnName(nameof(RegistroEvento.Data))
            .HasColumnType("DATETIME2");
        
        builder
            .Property(x => x.Evento)
            .HasColumnName(nameof(RegistroEvento.Evento))
            .HasMaxLength(100);
        
        builder
            .Property(x => x.DadosEvento)
            .HasColumnName(nameof(RegistroEvento.DadosEvento));

        builder
            .Property(x => x.PessoaId)
            .HasColumnName(nameof(RegistroEvento.PessoaId));

    }
}
