using Basket.API.DAL.Entities;
using Basket.API.PL.Models.DTOs;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;

namespace Basket.API.BL.Interfaces
{
    public interface IShoppingCartService
    {
        public Task<ShoppingCart> AddShoppingCartAsync(AddShoppingCartDto addShoppingCartDto);

        public Task<ServiceResult<ShoppingCartDto>> GetShoppingCartByIdAsync();

        public Task DeleteShoppingCartAsync();
    }
}
