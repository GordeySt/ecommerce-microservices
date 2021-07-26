using Catalog.API.DAL.Entities;
using Services.Common.ResultWrappers;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IProductRepository : IAsyncBaseRepository<Product>
    {
        public Task<ServiceResult> UpdateMainImageAsync(Product product, string photoUrl);
    }
}
