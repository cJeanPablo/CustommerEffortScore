using Ces.Api.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Ces.Api.Infrastructure.Context
{
    public class CesContext(DbContextOptions<CesContext> options) : DbContext(options)
    {
        public DbSet<TipoPergunta> TipoPerguntas { get; set; }
        public DbSet<Pergunta> Perguntas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(400)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CesContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
