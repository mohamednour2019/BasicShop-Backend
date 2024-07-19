using AutoMapper;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.DTO_S.Cart.ResponseDTOs;
using BasicShop.Core.DTO_S.Product.RequestDTOs;
using BasicShop.Core.DTO_S.Product.ResponseDTOs;
using BasicShop.Core.DTO_S.User.RequestDTOs;
using BasicShop.Core.DTO_S.User.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Infrastructure.Mapper
{
    public class Mapper:Profile
    {
        public Mapper() {
            Initialize();
        }

        private void Initialize()
        {
            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FristName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Cart.Id))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            //CreateMap<UserResponseDto, RegisterResponseDto>()
            //.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            //.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            //.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<AddProductRequestDto,Product>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));



            CreateMap<CartProduct, ProductCartResponsDto>()
            .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Cart.Id))
            .ForMember(dest => dest.CartTotalPrice, opt => opt.MapFrom(src => src.Cart.TotalPrice))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.CartTotalPrice, opt => opt.MapFrom(src => src.Cart.TotalPrice))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price));

        }
    }

}
