using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Infrastructure.Entities;
using System.Text;

namespace FarmlyCore.Application.MapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(e => e.CustomerId, v => v.MapFrom(src => src.FkCustomerId));

            CreateMap<UserDto, User>()
                .ForMember(e => e.Customer, v => v.Ignore());
        }
    }
}