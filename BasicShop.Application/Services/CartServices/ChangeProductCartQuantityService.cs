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
        private IGenericRepository<CartProduct> _genericRepository;
        private ICartRepository _cartRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ChangeProductCartQuantityService(IGenericRepository<CartProduct> genericRepository
            , ICartRepository cartRepository
            , IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _genericRepository = genericRepository;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ProductCartResponsDto>> perform(ChangeProductCartQuantityRequestDto? requestDto)
        {
            CartProduct cartProduct=await _cartRepository.GetCartProduct(requestDto.CartId,requestDto.ProductId);


            await _unitOfWork.BeginTransaction();
            try
            {
                if (requestDto.Quantity == 0)
                {
                    //update product
                    cartProduct.Product.QuantityInStock += cartProduct.Quantity;


                    //update Cart
                    cartProduct.Cart.TotalPrice -= cartProduct.UnitPrice;


                    //delete cart product
                    cartProduct.Quantity = 0;
                    cartProduct.UnitPrice = 0;
                }
                else
                {
                    int difference = requestDto.Quantity - cartProduct.Quantity;
                    if (difference > 0)//quantity increased
                    {
                        if (cartProduct.Product.QuantityInStock >= difference)
                        {

                            decimal increaseAmount = (difference * cartProduct.Product.Price);


                            //update product
                            cartProduct.Product.QuantityInStock -= difference;


                            //update cart product
                            cartProduct.Quantity += difference;
                            cartProduct.UnitPrice += increaseAmount;


                            //update Cart
                            cartProduct.Cart.TotalPrice += increaseAmount;
                        }
                        else
                        {
                            throw new ViolenceConstraintException("Quantity in Stock not enough!");
                        }
                    }
                    else
                    {
                        difference = Math.Abs(difference);//convert to positive
                        decimal decreasedAmount = difference * cartProduct.Product.Price;

                        //update product
                        cartProduct.Product.QuantityInStock += difference;


                        //update cart product
                        cartProduct.Quantity -= difference;
                        cartProduct.UnitPrice -= decreasedAmount;


                        //update Cart
                        cartProduct.Cart.TotalPrice -= decreasedAmount;
                    }
                }

               _genericRepository.Update(cartProduct);

            }catch(Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                throw new ViolenceConstraintException("Quantity in Stock not enough!");
            }
            await _unitOfWork.CommitTransaciton();

            ProductCartResponsDto response =_mapper.Map<ProductCartResponsDto>(cartProduct);
            return new ResponseModel<ProductCartResponsDto>(response, "Count changed", true);
        }
    }
}
