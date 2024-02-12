using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public interface IRepository<TEntity, TKey, EKey> where TEntity : BaseEntity<TKey, EKey>
    {
        Task<TEntity> Create(TEntity entity);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, EKey tenantId, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAll(EKey tenantId, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetById(TKey id, EKey tenantId, params Expression<Func<TEntity, object>>[] includes);
    }
}