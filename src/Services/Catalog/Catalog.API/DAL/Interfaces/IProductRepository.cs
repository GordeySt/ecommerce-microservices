using Catalog.API.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IProductRepository : IAsyncBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
    }
}
