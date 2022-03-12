using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.MapperProfile
{
    public class CustomerAddressProfile : Profile
    {
        public CustomerAddressProfile()
        {
            CreateMap<CustomerAddress, CustomerAddressDto>().ForMember(e => e.CustomerId, v => v.MapFrom(src => src.Customer.Id)).ReverseMap();
        }        
    }
}
