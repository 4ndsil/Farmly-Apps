using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.MapperProfile
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ForMember(e => e.AddressList, v => v.MapFrom(src => src.AddressList.Select(c => c.Id))).ReverseMap();
        }        
    }
}
