using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codeizi.Curso.RH.Infra.Data.Map
{
    public class OcorrenciaDeFeriasMap : IEntityTypeConfiguration<OcorrenciaDeFerias>
    {
        public void Configure(EntityTypeBuilder<OcorrenciaDeFerias> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnName("Id");

            builder.HasOne(x => x.Contrato)
                .WithMany(y => y.Ferias)
                .HasForeignKey(f => f.ContradoId)
                .IsRequired();

            builder.Property(x => x.PeriodoArquisitivo)
                .HasColumnName("PeriodoAquisitivo")
                .IsRequired();

            builder.Property(x => x.DataDeInicio)
                .HasColumnName("DataDeInicio")
                .IsRequired();

            builder.Property(x => x.DiasDeFerias)
                .HasColumnName("DiasDeFerias")
                .IsRequired();

            builder.Property(x => x.DiasDeAbono)
                .HasColumnName("DiasDeAboino")
                .IsRequired();
        }
    }
}