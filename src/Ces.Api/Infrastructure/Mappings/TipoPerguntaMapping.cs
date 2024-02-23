using Ces.Api.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ces.Api.Infrastructure.Mappings
{
    public class TipoPerguntaMapping : IEntityTypeConfiguration<TipoPergunta>
    {
        public void Configure(EntityTypeBuilder<TipoPergunta> builder)
        {
            builder.HasKey(tp => tp.Id);

            builder.Property(c => c.Tipo)
                .IsRequired()
                .HasColumnType("varchar(20)");
            builder.Property(c => c.Ativo);

            builder.HasMany(c => c.Perguntas)
                .WithOne(tp => tp.TipoPergunta)
                .HasForeignKey(p => p.IdTipoPergunta);

            builder.ToTable("TipoPergunta");
        }
    }
}
