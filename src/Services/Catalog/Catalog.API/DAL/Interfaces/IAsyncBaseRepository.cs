using Catalog.API.DAL.Entities;
using Services.Common.ResultWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IAsyncBaseRepository<T> where T : EntityBase
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public IQueryable<T> GetAllQueryable();

        public Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate);

        public Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true);

        public Task<T> GetByIdAsync(Guid id, bool disableTracking = true);

        public Task<ServiceResult<T>> AddAsync(T entity);

        public Task<ServiceResult> UpdateAsync(T entity);

        public Task<ServiceResult> DeleteAsync(T entity);
    }
}

