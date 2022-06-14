﻿using AutoMapper;
using FarmlyCore.Application.DTOs.Adverts;
using FarmlyCore.Infrastructure.Entities;

namespace FarmlyCore.Application.MapperProfile
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<Advert, AdvertDto>()
                .ForMember(e => e.Seller, v => v.MapFrom(src => src.Seller))
                .ForMember(e => e.Category, v => v.MapFrom(src => src.Category))
                .ForMember(e => e.PickupPointId, v => v.MapFrom(src => src.FkPickupPointId))
                .ForMember(e => e.AdvertItems, v => v.MapFrom(src => src.AdvertItems));

            CreateMap<AdvertDto, Advert>()
               .ForMember(e => e.Seller, v => v.Ignore())
               .ForMember(e => e.PickupPoint, v => v.Ignore())
               .ForMember(e => e.Category, v => v.MapFrom(src => src.Category))               
               .ForMember(e => e.AdvertItems, v => v.MapFrom(src => src.AdvertItems));

            CreateMap<AdvertItem, AdvertItemDto>().ReverseMap();

            CreateMap<CreateAdvertDto, Advert>().ReverseMap();

            CreateMap<CreateAdvertItemDto, AdvertItem>().ReverseMap();
        }
    }
}