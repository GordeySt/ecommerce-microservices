using AutoMapper;
using Basket.API.DAL.Entities;
using Basket.API.PL.Models.DTOs;

namespace Basket.API.BL.Mappers
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<AddShoppingCartDto, ShoppingCart>()
                .ForMember(
                    dest => dest.ShoppingCartItems,
                    ex => ex.MapFrom(en => en.ShoppingCartItems)
                );

            CreateMap<ShoppingCart, ShoppingCartDto>()
                .ForMember(
                   dest => dest.ShoppingCartItems,
                   ex => ex.MapFrom(en => en.ShoppingCartItems)
                );

            CreateMap<ShoppingCartItemDto, ShoppingCartItem>().ReverseMap();
        }
    }
}
