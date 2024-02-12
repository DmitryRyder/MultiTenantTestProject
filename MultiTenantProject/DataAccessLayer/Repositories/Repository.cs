using DataAccessLayer.Contexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class Repository<TEntity, TKey, EKey> : IRepository<TEntity, TKey, EKey>
        where TEntity : BaseEntity<TKey, EKey>
    {
        private readonly ClientBaseContext _context;

        public Repository(ClientBaseContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> GetById(TKey id, EKey tenantId, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id) && e.TenantId.Equals(tenantId));
        }

        public async Task<IEnumerable<TEntity>> GetAll(EKey tenantId, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>()
                .Where(e => e.TenantId.Equals(tenantId))
                .AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, EKey tenantId, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>()
                .Where(predicate)
                .Where(e => e.TenantId.Equals(tenantId))
                .AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
    }
}