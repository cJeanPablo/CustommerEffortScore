using Ces.Api.Business.Interfaces;
using Ces.Api.Business.Models;
using Ces.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Ces.Api.Infrastructure.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly CesContext Db;
        protected readonly DbSet<TEntity> DbSet;
        private readonly IMemoryCache _cache;

        protected Repository(CesContext db, IMemoryCache cache)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
            _cache = cache;

        }
        public virtual async Task<bool> Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await Db.SaveChangesAsync();
            return true;

        }

        public virtual async Task<bool> Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await Db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToArrayAsync();
        }
        public virtual async Task<bool> Excluir(TEntity entity)
        {
            DbSet.Remove(entity);
            await Db.SaveChangesAsync();
            return true;
        }



        public void Dispose()
        {
            Db?.Dispose();
        }

        public Task<TEntity[]> BuscarComCache(string nomeCache, int tempoExpiracao, Expression<Func<TEntity, bool>> predicate)
        {
            var cache = _cache.GetOrCreate(nomeCache, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(tempoExpiracao);

                return await DbSet.AsNoTracking().Where(predicate).ToArrayAsync();
            });
            return cache;
        }
    }

}
