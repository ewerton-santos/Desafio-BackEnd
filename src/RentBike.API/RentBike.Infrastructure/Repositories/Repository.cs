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

        public async Task<IEnumerable<TEntity>> GetAll() => await _entities.ToListAsync();
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