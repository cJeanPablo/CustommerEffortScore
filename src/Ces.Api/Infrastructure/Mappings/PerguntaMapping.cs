using Ces.Api.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ces.Api.Infrastructure.Mappings
{
    public class PerguntaMapping : IEntityTypeConfiguration<Pergunta>
    {
        public void Configure(EntityTypeBuilder<Pergunta> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.IdTipoPergunta);

            builder.Property(p => p.TxtPergunta)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(p => p.NotaMaxima);
            builder.Property(p => p.Ordem);
            builder.Property(p => p.Ativo);


            builder.ToTable("Pergunta");
        }
    }
}
