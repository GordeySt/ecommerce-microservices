using AutoMapper;
using Identity.Domain.Entities;
using Identity.Grpc.Protos;

namespace Identity.Grpc.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserModel>().ReverseMap();
        }
    }
}
