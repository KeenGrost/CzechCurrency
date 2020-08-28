using Common.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Data.EF
{
    /// <summary>
    /// Базовый репозиторий для сущности типа {TEntity}
    /// </summary>
    /// <typeparam name="TEntity">Сущность, хранящаяся в репозитории</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private DbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        protected BaseRepository(DbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        /// <summary>
        /// Получить подключение к БД
        /// </summary>
        /// <returns></returns>
        protected DbConnection GetDbConnection()
        {
            return DbContext.Database.GetDbConnection();
        }

        public virtual async Task<TEntity> Find(params object[] id)
        {
            return await DbSet.FindAsync(id);
        }

        //public virtual async Task<TEntity[]> FindAll(IEnumerable<long> ids, bool noTracking = false)
        //{
        //    var parameter = Expression.Parameter(typeof(TEntity), "x");
        //    var property = Expression.Property(parameter, "Id");

        //    ConstantExpression constant = Expression.Constant(ids);

        //    Expression binaryExpression = Expression.Call(constant, "Contains", Type.EmptyTypes, property);
        //    var lambda = Expression.Lambda<Func<TEntity, bool>>(binaryExpression, parameter);

        //    var query = DbSet.Where(lambda);

        //    if (!noTracking)
        //    {
        //        return await query
        //            .ToArrayAsync();
        //    }

        //    return await query
        //        .AsNoTracking()
        //        .ToArrayAsync();
        //}

        public virtual async Task<TEntity[]> GetAll(bool noTracking = false)
        {
            var query = DbSet.OrderBy(t => t);

            if (!noTracking)
            {
                return await query
                    .ToArrayAsync();
            }

            return await query
                .AsNoTracking()
                .ToArrayAsync();
        }

        public virtual async Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> filter, bool noTracking = false)
        {
            var query = DbSet.Where(filter).OrderBy(t => t);

            if (!noTracking)
            {
                return await query
                    .ToArrayAsync();
            }

            return await query
                .AsNoTracking()
                .ToArrayAsync();
        }

        public virtual async Task<int> Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> Update(TEntity entity)
        {
            var state = DbContext.Entry(entity).State;
            if (state == EntityState.Detached)
                DbSet.Update(entity);

            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                var state = DbContext.Entry(entity).State;
                if (state == EntityState.Detached)
                    DbSet.Update(entity);
            }

            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> Delete(params object[] id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
                return 0;

            DbSet.Remove(entity);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> Delete(Expression<Func<TEntity, bool>> filter)
        {
            DbSet.RemoveRange(await DbSet.Where(filter).ToListAsync());
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> Delete(TEntity entity)
        {
            DbSet.Attach(entity);
            DbSet.Remove(entity);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteRange(IEnumerable<TEntity> entities)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            DbSet.AttachRange(entities);
            // ReSharper disable once PossibleMultipleEnumeration
            DbSet.RemoveRange(entities);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }

        public virtual async Task<int> SaveChanges()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
