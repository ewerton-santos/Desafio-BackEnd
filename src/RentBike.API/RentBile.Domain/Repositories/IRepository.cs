using System.Linq.Expressions;

namespace RentBike.Domain.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity> GetById(TId id);
        Task<TEntity> GetById(TId id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
    }
}
