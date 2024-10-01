using Domin.Interfaces.Reposatore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
           where TEntity : class
        where TContext : DbContext, new()
    {
        protected readonly TContext _context;
        private DbSet<TEntity> _set;
        public GenericRepository(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = _context.Set<TEntity>();
        }
        public TEntity Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return _context.Set<TEntity>().Add(entity).Entity;
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(int id)
        {
            var entity =  _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task DeleteWhere(Expression<Func<TEntity, bool>>? predicate = null)
        {
            if (predicate != null)
                _context.Set<TEntity>().RemoveRange(await _context.Set<TEntity>().Where(predicate).ToListAsync());
        }

        public List<TEntity> GetAll()
        {
            var entities =  _context.Set<TEntity>().ToList();
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            return entities;
        }

        public async Task<List<TEntity>> GetAllAsNoTrackingAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {

            var entities = await _context.Set<TEntity>()
                .Where(predicate).AsNoTracking().ToListAsync();
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            return entities;
        }
        public async Task<List<TEntity>> GetAllAsNoTrackingAsync()
        {

            var entities = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            return entities;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            return entities;
        }

        public TEntity GetById(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            _context.Entry(entity).State = EntityState.Detached;

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(int? id)
        {
            if(id==null)
				throw new ArgumentNullException(nameof(id));
			var entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return entity;
        }
        public async Task<bool> SaveEntitiesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
