using Catalog.API.BL.ResultWrappers;
using Catalog.API.DAL.Entities;
using Services.Common.Models;
using System;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IAsyncBaseRepository<T> where T : EntityBase
    {
		Task<PagedList<T>> GetAllItemsAsync(PagingParams pagingParams);
		Task<T> GetItemByIdAsync(Guid id);
		Task AddItemAsync(T entity);
		Task<ServiceResult> UpdateItemAsync(T entity);
		Task<ServiceResult> DeleteItemAsync(Guid Id);
	}
}

