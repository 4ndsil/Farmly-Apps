using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.MapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(e => e.CustomerId, v => v.MapFrom(src => src.FkCustomerId)).ReverseMap();

            CreateMap<UserDto, User>()
                .ForMember(e => e.Customer, v => v.Ignore());
        }
    }
}
