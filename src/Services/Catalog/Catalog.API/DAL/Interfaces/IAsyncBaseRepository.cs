using Catalog.API.BL.ResultWrappers;
using Catalog.API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IAsyncBaseRepository<T> where T : EntityBase
    {
		Task<IEnumerable<T>> GetAllItemsAsync();
		Task<T> GetItemByIdAsync(Guid id);
		Task AddItemAsync(T entity);
		Task<ServiceResult> UpdateItemAsync(T entity);
		Task<ServiceResult> DeleteItemAsync(Guid Id);
	}
}

