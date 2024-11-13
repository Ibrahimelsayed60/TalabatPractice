﻿using AutoMapper;
using Talabat.API.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.API.Helpers
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto> ()
                .ForMember(d => d.ProductType, O => O.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.ProductBrand, O => O.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>() );

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();

            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, O => O.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(s =>s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, O => O.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, O => O.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, O => O.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());
        }

    }
}
