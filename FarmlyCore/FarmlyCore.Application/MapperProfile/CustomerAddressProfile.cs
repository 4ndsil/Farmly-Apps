using AutoMapper;
using FarmlyCore.Application.DTOs.Customer;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.MapperProfile
{
    public class CustomerAddressProfile : Profile
    {
        public CustomerAddressProfile()
        {
            CreateMap<CustomerAddress, CustomerAddressDto>()
               .ForMember(e => e.FKCustomerId, v => v.MapFrom(src => src.FkCustomerId));

            CreateMap<CustomerAddressDto, CustomerAddress>()
                .ForMember(e => e.FkCustomerId, v => v.MapFrom(src => src.FKCustomerId))
                .ForMember(e => e.Customer, v => v.Ignore());           
        }
    }
}
