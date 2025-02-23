using System.Linq.Expressions;
using Domain.Models;

namespace HotelRv.Infrastructure.Persistence.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        int Add(TEntity entity);
        Task<int> AddAsync(TEntity entity);
        int AddOrUpdate(TEntity entity);
        Task<int> AddOrUpdateAsync(TEntity entity);
        IQueryable<TEntity> AsQueryable();
        Task BulkAdd(IEnumerable<TEntity> entities);
        Task BulkDelete(Expression<Func<TEntity, bool>> predicate);
        Task BulkDelete(IEnumerable<TEntity> entities);
        Task BulkDeleteById(IEnumerable<Guid> ids);
        Task BulkUpdate(IEnumerable<TEntity> entities);
        int Delete(Guid id);
        int Delete(TEntity entity);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteAsync(TEntity entity);
        bool DeleteRange(Expression<Func<TEntity, bool>> predicate);
        Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetAll(bool noTracking = true);
        Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<bool> IsDateRangeAvailableAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, DateTime startDate, DateTime endDate, string[] excludedStatuses = null) where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        int Update(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
    }
}