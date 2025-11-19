using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TrackNStock.Api.Models.DomainModel;
using TrackNStock.Api.Models.DTOs;

namespace TrackNStock.Api.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDtoForPublic>();
            CreateMap<AddProductRequestDto, Product>();
            CreateMap<UpdateProductRequestDto, Product>();

            CreateMap<Product, ProductDto>();

            CreateMap<Order, OrderDto>();
            CreateMap<AddOrderRequestDto, Order>();
            CreateMap<UpdateOrderRequestDto, Order>();

            CreateMap<Sales, SalesDto>();
            CreateMap<AddSalesRequestDto, Sales>();
            CreateMap<UpdateSalesRequestDto, Sales>();

            CreateMap<ShopOwner, ShopOwnerDto>();

            //CreateMap<Product>
        }
    }
}