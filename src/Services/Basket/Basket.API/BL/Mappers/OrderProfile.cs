using AutoMapper;
using Basket.API.DAL.Entities;
using Basket.API.PL.Models.DTOs;

namespace Basket.API.BL.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(
                    dest => dest.BillingAddress,
                    ex => ex.MapFrom(en => en.BillingAddress)
                )
                .ForMember(
                    dest => dest.Payment,
                    ex => ex.MapFrom(en => en.Payment)
                );
        }
    }
}
