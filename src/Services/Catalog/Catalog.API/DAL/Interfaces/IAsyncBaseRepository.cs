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
        Task<List<T>> GetAllAsync();

        IQueryable<T> GetAllQueryable();

        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetQueryable(ref IQueryable<T> entity, Expression<Func<T, bool>> expression);

        Task<ServiceResult<T>> AddAsync(T entity);

        Task<ServiceResult> UpdateAsync(T entity);

        Task<ServiceResult> DeleteAsync(T entity);
    }
}

