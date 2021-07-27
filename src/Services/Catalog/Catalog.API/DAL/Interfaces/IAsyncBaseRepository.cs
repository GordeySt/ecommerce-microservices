using Catalog.API.DAL.Entities;
using Services.Common.ResultWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IAsyncBaseRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public IQueryable<T> GetAllQueryable();

        public Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate);

        public IQueryable<T> GetQueryable(ref IQueryable<T> entity, Expression<Func<T, bool>> expression);

        public Task<ServiceResult<T>> AddAsync(T entity);

        public Task<ServiceResult> UpdateAsync(T entity);

        public Task<ServiceResult> DeleteAsync(T entity);
    }
}

