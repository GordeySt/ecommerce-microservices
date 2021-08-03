using AutoMapper;
using Basket.API.BL.Interfaces;
using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Redis;
using Basket.API.PL.Models.DTOs;
using Services.Common.Constatns;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;

namespace Basket.API.BL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, 
            ICurrentUserService currentUserService, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<ShoppingCart> AddShoppingCartAsync(AddShoppingCartDto shoppingCartDto)
        {
            shoppingCartDto.Id = new Guid(_currentUserService.UserId);

            var shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartDto);

            return await _shoppingCartRepository.AddAsync(shoppingCart);
        }

        public async Task<ServiceResult<ShoppingCartDto>> GetShoppingCartByIdAsync()
        {
            var currentUserId = _currentUserService.UserId;

            var shoppingCart = await _shoppingCartRepository.GetAsync(currentUserId);

            if (shoppingCart is null)
            {
                return new ServiceResult<ShoppingCartDto>(ServiceResultType.NotFound, 
                    ExceptionConstants.NotFoundItemMessage);
            }

            var shoppingCartDto = _mapper.Map<ShoppingCartDto>(shoppingCart);

            return new ServiceResult<ShoppingCartDto>(ServiceResultType.Success, shoppingCartDto);
        }
    }
}
