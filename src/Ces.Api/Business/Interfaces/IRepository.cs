using Ces.Api.Business.Models;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Ces.Api.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<bool> Adicionar(TEntity entity);
        Task<bool> Atualizar(TEntity entity);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Excluir(TEntity entity);
        Task<TEntity[]> BuscarComCache(string nomeCache, int tempoExpiracao, Expression<Func<TEntity, bool>> predicate);
    }
}
