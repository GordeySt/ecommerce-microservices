using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Repositories
{
    public class AsyncBaseRepository<T> : IAsyncBaseRepository<T> where T 
        : EntityBase
    {
        protected readonly ApplicationDbContext DatabaseContext;
        private readonly DbSet<T> _entity;

        protected AsyncBaseRepository(ApplicationDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
            _entity = databaseContext.Set<T>();
        }

        public IQueryable<T> GetAllQueryable() => _entity.AsQueryable();

        public async Task<IEnumerable<T>> GetAllAsync() => await _entity.ToListAsync();

        public async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate) => 
            await _entity.Where(predicate).ToListAsync();

        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> expression) => 
            _entity.Where(expression);

        public async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true)
        {
            IQueryable<T> query = _entity;

            if (disableTracking) query = query.AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id, bool disableTracking = true)
        {
            if (disableTracking)
            {
                return await _entity.FirstOrDefaultAsync(x => x.Id == id);
            }

            return await _entity.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ServiceResult<T>> AddAsync(T entity)
        {
            _entity.Add(entity);

            var success = await DatabaseContext.SaveChangesAsync() > 0;

            if (!success)
            {
                return new ServiceResult<T>(ServiceResultType.InternalServerError,
                    ExceptionConstants.ProblemCreatingItemMessage);
            }

            return new ServiceResult<T>(ServiceResultType.Success, entity);
        }

        public async Task<ServiceResult> UpdateAsync(T entity)
        {
            _entity.Update(entity);

            var success = await DatabaseContext.SaveChangesAsync() > 0;

            if (!success)
            {
                return new ServiceResult(ServiceResultType.InternalServerError,
                    ExceptionConstants.ProblemUpdatingItemMessage);
            }

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResult> DeleteAsync(T entity)
        {
            _entity.Remove(entity);

            var success = await DatabaseContext.SaveChangesAsync() > 0;

            if (!success)
            {
                return new ServiceResult(ServiceResultType.InternalServerError, 
                    ExceptionConstants.ProblemDeletingItemMessage);
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
