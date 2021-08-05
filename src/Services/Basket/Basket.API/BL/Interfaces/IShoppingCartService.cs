using Basket.API.DAL.Entities;
using Basket.API.PL.Models.DTOs;
using Services.Common.ResultWrappers;
using System.Threading.Tasks;

namespace Basket.API.BL.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> AddShoppingCartAsync(AddShoppingCartDto addShoppingCartDto);

        Task<ServiceResult<ShoppingCartDto>> GetShoppingCartByIdAsync();

        Task DeleteShoppingCartAsync();
    }
}
