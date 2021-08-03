using Basket.API.DAL.Entities;
using Basket.API.PL.Models.DTOs;
using System.Threading.Tasks;

namespace Basket.API.BL.Interfaces
{
    public interface IShoppingCartService
    {
        public Task<ShoppingCart> AddShoppingCartAsync(AddShoppingCartDto addShoppingCartDto);
    }
}
