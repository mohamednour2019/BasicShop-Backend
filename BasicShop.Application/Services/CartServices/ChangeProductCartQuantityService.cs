using AutoMapper;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Core.DTO_S.Cart.RequestDTOs;
using BasicShop.Core.DTO_S.Cart.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.CartInterfaces;
using BasicShop.Shared.CustomExceptions;
using Microsoft.AspNetCore.Identity;

namespace BasicShop.Application.Services.CartServices
{
    public class ChangeProductCartQuantityService : IChangeProductCartQuantityService
    {
        private IGenericRepository<CartProduct> _cartProductRepository;
        private IGenericRepository<Cart> _cartRepository;
        private IGenericRepository<Product> _productRepository;
        private UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ChangeProductCartQuantityService(IGenericRepository<CartProduct> cartProductRepository
            , IGenericRepository<Cart> cartRepository
            , IGenericRepository<Product> productRepository
            , UserManager<User> userManager
            , IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _cartProductRepository = cartProductRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ProductCartResponsDto>> perform(ChangeProductCartQuantityRequestDto? requestDto)
        {
            Cart cart = await _cartRepository.GetByConditionAsync(x => x.UserId == requestDto.UserId);
            CartProduct cartProduct=await _cartProductRepository.GetByConditionAsync(x=>x.CartId==cart.Id&&x.ProductId==requestDto.ProductId);
            Product product = await _productRepository.GetByIdAsync(requestDto.ProductId);


            await _unitOfWork.BeginTransaction();
            try
            {
                if (requestDto.Quantity == 0)
                {
                    //update product
                    product.QuantityInStock += cartProduct.Quantity;
                    _productRepository.Update(product);


                    //update Cart
                    cart.TotalPrice -= cartProduct.UnitPrice;
                    _cartRepository.Update(cart);



                    //delete cart product
                    cartProduct.Quantity = 0;
                    cartProduct.UnitPrice = 0;
                    _cartProductRepository.Update(cartProduct);


                }
                else
                {
                    int difference = requestDto.Quantity - cartProduct.Quantity;
                    if (difference > 0)//quantity increased
                    {
                        if (product.QuantityInStock >= difference)
                        {

                            decimal increaseAmount = (difference * product.Price);


                            //update product
                            product.QuantityInStock -= difference;
                            _productRepository.Update(product);


                            //update cart product
                            cartProduct.Quantity += difference;
                            cartProduct.UnitPrice += increaseAmount;
                            _cartProductRepository.Update(cartProduct);


                            //update Cart
                            cart.TotalPrice += increaseAmount;
                            _cartRepository.Update(cart);


                        }
                        else
                        {
                            throw new ViolenceConstraintException("Quantity in Stock not enough!");
                        }
                    }
                    else
                    {
                        difference = Math.Abs(difference);//convert to positive
                        decimal decreasedAmount = difference * product.Price;

                        //update product
                        product.QuantityInStock += difference;
                        _productRepository.Update(product);



                        //update cart product
                        cartProduct.Quantity -= difference;
                        cartProduct.UnitPrice -= decreasedAmount;
                        _cartProductRepository.Update(cartProduct);


                        //update Cart
                        cart.TotalPrice -= decreasedAmount;
                        _cartRepository.Update(cart);
                    }
                }

            }catch(Exception ex)
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
            return new ResponseModel<ProductCartResponsDto>(response, "Count changed", true);
        }
    }
}
