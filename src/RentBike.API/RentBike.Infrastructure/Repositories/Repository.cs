using Microsoft.EntityFrameworkCore;
using RentBike.Domain.Repositories;
using System.Linq.Expressions;

namespace RentBike.Infrastructure.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task<TEntity> GetById(TId id)
        {
            if (typeof(TId) == typeof(Guid))
                return await _entities.FindAsync(Guid.Parse(id.ToString()));
            else
                return await _entities.FindAsync(id);
        }

        public async Task<TEntity> GetById(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _entities;
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            var idProperty = typeof(TEntity).GetProperty("Id");
            var idParameter = Expression.Parameter(typeof(TEntity), "e");
            var idExpression = Expression.Property(idParameter, idProperty);
            var idValue = Guid.Parse(id.ToString());
            var idEqualsExpression = Expression.Equal(idExpression, Expression.Constant(idValue));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(idEqualsExpression, idParameter);
            return await query.FirstOrDefaultAsync(lambda);
        }

        public async Task<IEnumerable<TEntity>> GetAll() => await _entities.ToListAsync();
        public async Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _entities;
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            return await query.ToListAsync();
        }        
        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate) => await _entities.Where(predicate).ToListAsync();
        
        public async Task Add(TEntity entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(TEntity entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Remove(TEntity entity) 
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}