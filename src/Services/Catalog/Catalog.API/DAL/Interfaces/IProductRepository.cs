using Catalog.API.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IProductRepository : IAsyncBaseRepository<Product>
    {
        Task<IReadOnlyCollection<Product>> GetProductsByCategory(string category);
    }
}
