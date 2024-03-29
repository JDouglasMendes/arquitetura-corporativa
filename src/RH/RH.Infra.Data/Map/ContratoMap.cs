﻿using RH.Domain.Colaboradores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RH.Infra.Data.Map
{
    public class ContratoMap : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            builder.Property(x => x.Id)
                            .HasColumnName("Id");

            builder.Property(x => x.DataInicio)
                .HasColumnName("DataInicio")
                .IsRequired();

            builder.Property(x => x.DataFim)
                .HasColumnName("DataFim")
                .IsRequired(false);

            builder.Property(x => x.SalarioContratual)
                .HasColumnName("SalarioContratual")
                .IsRequired();

            builder.HasOne(x => x.Colaborador)
                .WithMany(c => c.Contratos)
                .HasForeignKey(c => c.ColaboradorId)
                .IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Contrato.Ferias));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Property);

            builder.HasMany(x => x.Ferias)
                .WithOne(c => c.Contrato)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}