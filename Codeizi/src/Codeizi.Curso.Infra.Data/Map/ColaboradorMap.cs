using Codeizi.Curso.RH.Domain.Colaboradores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codeizi.Curso.RH.Infra.Data.Map
{
    public class ColaboradorMap : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnName("Id");

            builder.OwnsOne(x => x.Nome, nome =>
            {
                nome.Property(n => n.Nome)
                        .HasColumnName("Nome")
                        .HasMaxLength(100)
                        .IsRequired();

                nome.Property(s => s.Sobrenome)
                        .HasColumnName("Sobrenome")
                        .HasMaxLength(100)
                        .IsRequired();
            });

            builder.Property(x => x.DataDeNascimento)
                .HasColumnName("DataDeNascimento")
                .IsRequired();

            builder.Property(x => x.ObservacaoContratual)
                .HasMaxLength(100);

            var navigation = builder.Metadata.FindNavigation(nameof(Colaborador.Contratos));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Property);

            builder.HasMany(x => x.Contratos)
                .WithOne(c => c.Colaborador)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}