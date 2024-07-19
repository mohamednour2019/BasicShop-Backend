using AutoMapper;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Core.DTO_S.Cart.RequestDTOs;
using BasicShop.Core.DTO_S.Cart.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.CartInterfaces;
using BasicShop.Shared.CustomExceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Application.Services.CartServices
{
    public class AddProductToCartService : IAddProductToCartService
    {
        private IGenericRepository<CartProduct> _cartProductRepository;
        private IGenericRepository<Cart> _cartRepository;
        private IGenericRepository<Product> _productRepository;
        private UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public AddProductToCartService(IGenericRepository<CartProduct> cartProductRepository
            , IGenericRepository<Cart> cartRepository
            , IGenericRepository<Product>productRepository
            , IUnitOfWork unitOfWork
            , IMapper mapper, UserManager<User> userManager)
        {
            _cartProductRepository = cartProductRepository;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _productRepository = productRepository;
        }

        public async Task<ResponseModel<ProductCartResponsDto>> perform(AddProductToCartRequestDto? requestDto)
        {
            Cart cart=await _cartRepository.GetByIdAsync(requestDto.CartId);
            Product product = await _productRepository.GetByIdAsync(requestDto.ProductId);
            if (product.QuantityInStock < 1)
            {
                throw new ViolenceConstraintException("Sorry the Product Out Of Stock!");
            }
            if(cart is null || product is null)
            {
                throw new ViolenceConstraintException("Product or Cart not found");
            }
            CartProduct cartProduct = new CartProduct()
            {
                CartId = cart.Id,
                ProductId = requestDto.ProductId,
                Quantity = 1,
                UnitPrice = product.Price
            };
            await _unitOfWork.BeginTransaction();
            try
            {
                await _cartProductRepository.AddAsync(cartProduct);
                //update cart total price
                cart.TotalPrice += cartProduct.UnitPrice;
                _cartRepository.Update(cart);

                //update product Quantity
                product.QuantityInStock -= 1;
                _productRepository.Update(product);

            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                throw new Exception(ex.Message);

            }
            await _unitOfWork.CommitTransaciton();


            ProductCartResponsDto response = new ProductCartResponsDto()
            {
                CartId = cart.Id,
                CartTotalPrice = cart.TotalPrice,
                ProductId = requestDto.ProductId,
                ProductName = product.Name,
                ProductPrice = product.Price,
                Quantity = cartProduct.Quantity,
                UnitPrice = cartProduct.UnitPrice
            };
            return new ResponseModel<ProductCartResponsDto>(response, "Prodcut Added To Cart", true);



        }
    }
}
