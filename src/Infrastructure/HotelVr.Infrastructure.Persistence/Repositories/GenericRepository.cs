using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRv.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> entity => dbContext.Set<TEntity>();

        public GenericRepository(DbContext dbContext, DbSet<TEntity> dbSet)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbSet;
        }

        #region Insert Methods


        public async Task<bool> IsDateRangeAvailableAsync<TEntity>(
     Expression<Func<TEntity, bool>> predicate,
     DateTime startDate,
     DateTime endDate,
     string[] excludedStatuses = null) where TEntity : class
        {
            // Varsayılan olarak dışlanacak durumlar
            excludedStatuses ??= new[] { "Cancelled", "Completed" };

            // Reflection ile property bilgilerini al
            var statusProperty = typeof(TEntity).GetProperty("Status");
            var startDateProperty = typeof(TEntity).GetProperty("StartDate");
            var endDateProperty = typeof(TEntity).GetProperty("EndDate");

            // Eğer gerekli propertyler yoksa exception fırlat
            if (statusProperty == null || startDateProperty == null || endDateProperty == null)
            {
                throw new InvalidOperationException("Gerekli propertyler bulunamadı");
            }

            // Tarih çakışması kontrolü için dinamik expression oluştur
            var parameter = Expression.Parameter(typeof(TEntity), "x");

            // Status kontrolü
            var statusMethod = Expression.Call(
                statusProperty.GetMethod,
                parameter
            );
            var statusNotExcludedExpression = Expression.Not(
                Expression.Call(
                    typeof(Enumerable).GetMethod("Contains", new[] { typeof(string[]), typeof(string) }),
                    Expression.Constant(excludedStatuses),
                    statusMethod
                )
            );

            // Tarih çakışması kontrolü
            var startDateMethod = Expression.Call(
                startDateProperty.GetMethod,
                parameter
            );
            var endDateMethod = Expression.Call(
                endDateProperty.GetMethod,
                parameter
            );

            var dateOverlapExpression = Expression.OrElse(
                Expression.AndAlso(
                    Expression.GreaterThanOrEqual(Expression.Constant(startDate), startDateMethod),
                    Expression.LessThan(Expression.Constant(startDate), endDateMethod)
                ),
                Expression.AndAlso(
                    Expression.GreaterThan(Expression.Constant(endDate), startDateMethod),
                    Expression.LessThanOrEqual(Expression.Constant(endDate), endDateMethod)
                )
            );

            // Tüm koşulları birleştir
            var combinedPredicate = Expression.AndAlso(
                statusNotExcludedExpression,
                dateOverlapExpression
            );

            var lambda = Expression.Lambda<Func<TEntity, bool>>(
                combinedPredicate,
                parameter
            );

            // Orijinal predicate ile birleştir
            var finalPredicate = Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(
                    predicate.Body,
                    lambda.Body
                ),
                predicate.Parameters[0]
            );

            // Sorguyu çalıştır
            var query = dbContext.Set<TEntity>().Where(finalPredicate);
            return !await query.AnyAsync();

            // Reflection ile property değeri alma yardımcı metodu
            TProperty GetPropertyValue<TEntity, TProperty>(
           TEntity entity,
           string propertyName) where TEntity : class
            {
                return (TProperty)entity.GetType()
                    .GetProperty(propertyName)
                    ?.GetValue(entity);
            };
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await this.entity.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        public virtual int Add(TEntity entity)
        {
            this.entity.Add(entity);
            return dbContext.SaveChanges();
        }

        #endregion

        #region Update Methods

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            return await dbContext.SaveChangesAsync();
        }

        public virtual int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        #endregion

        #region Delete Methods

        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }

            this.entity.Remove(entity);

            return dbContext.SaveChangesAsync();
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            var entity = this.entity.Find(id);
            return DeleteAsync(entity);
        }

        public virtual int Delete(Guid id)
        {
            var entity = this.entity.Find(id);
            return Delete(entity);
        }

        public virtual int Delete(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }

            this.entity.Remove(entity);

            return dbContext.SaveChanges();
        }

        public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            dbContext.RemoveRange(entity.Where(predicate));
            return dbContext.SaveChanges() > 0;
        }

        public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            dbContext.RemoveRange(predicate);
            return await dbContext.SaveChangesAsync() > 0;
        }

        #endregion

        #region AddOrUpdate Methods

        public virtual Task<int> AddOrUpdateAsync(TEntity entity)
        {
            // Check the entity with the id already tracked
            if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            {
                dbContext.Update(entity);
            }

            return dbContext.SaveChangesAsync();
        }

        public virtual int AddOrUpdate(TEntity entity)
        {
            if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            {
                dbContext.Update(entity);
            }

            return dbContext.SaveChanges();
        }

        #endregion

        #region Get Methods

        public virtual IQueryable<TEntity> AsQueryable() => entity.AsQueryable();

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return query;
        }
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }
        public virtual async Task<List<TEntity>> GetAll(bool noTracking = true)
        {
            IQueryable<TEntity> query = entity;

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity found = await entity.FindAsync(id);

            if (found == null)
                return null;

            if (noTracking)
                dbContext.Entry(found).State = EntityState.Detached;

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                dbContext.Entry(found).Reference(include).Load();
            }

            return found;

        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();

        }
        #endregion

        #region Bulk Methods

        public virtual Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if (ids != null && !ids.Any())
                return Task.CompletedTask;

            dbContext.RemoveRange(entity.Where(i => ids.Contains(i.Id)));
            return dbContext.SaveChangesAsync();
        }

        public virtual Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            // predicate ifadesine göre entity'leri bulur ve siler
            var entitiesToDelete = entity.Where(predicate).ToList();

            if (entitiesToDelete == null || !entitiesToDelete.Any())
                return Task.CompletedTask;

            dbContext.RemoveRange(entitiesToDelete);
            return dbContext.SaveChangesAsync();
        }

        public virtual Task BulkDelete(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                return Task.CompletedTask;

            dbContext.RemoveRange(entities);
            return dbContext.SaveChangesAsync();
        }

        public virtual Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                return Task.CompletedTask;

            foreach (var entityItem in entities)
            {
                dbContext.Update(entityItem);
            }

            return dbContext.SaveChangesAsync();
        }

        public virtual Task BulkAdd(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                return Task.CompletedTask;

            foreach (var entityItem in entities)
            {
                entity.Add(entityItem);
            }

            return dbContext.SaveChangesAsync();
        }
       

        #endregion

        #region SaveChanges Methods

        public Task<int> SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        #endregion


        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {
                foreach (var includeItem in includes)
                {
                    query = query.Include(includeItem);
                }
            }

            return query;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate); // Veritabanındaki verileri belirtilen koşula göre filtreler
        }

    }
}
