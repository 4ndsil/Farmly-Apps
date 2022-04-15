using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.MapperProfile
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, Customer>().ForMember(e => e.CustomerAddresses, v => v.MapFrom(src => src.CustomerAddresses));

            CreateMap<Customer, CustomerDto>().ForMember(e => e.CustomerAddresses, v => v.MapFrom(src => src.CustomerAddresses));
        }
    }
}
