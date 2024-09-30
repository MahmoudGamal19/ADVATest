using System.Linq.Expressions;

namespace Domin.Interfaces.Reposatore
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        Task AddAsync(TEntity entity);
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity entity);
        void Delete(int id);
        Task DeleteByIdAsync(int id);
        Task DeleteWhere(Expression<Func<TEntity, bool>>? predicate = null);
        Task<bool> SaveEntitiesAsync();
    }
}
