using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Core.DTO_S.Cart.RequestDTOs;
using BasicShop.Core.ServiceInterfaces.CartInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Application.Services.CartServices
{
    public class DeleteCartProductService : IDeleteCartProductService
    {
        private IGenericRepository<CartProduct> _cartProductRepository;
        private IUnitOfWork _unitOfWork;
        private ICartRepository _cartRepository;

        public DeleteCartProductService(IGenericRepository<CartProduct> cartProductRepository
            , IUnitOfWork unitOfWork
            , ICartRepository cartRepository)
        {
            _cartProductRepository = cartProductRepository;
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
        }

        public async Task<ResponseModel<object>> perform(DeleteCartProductRequestDto? requestDto)
        {
            CartProduct targetCartProduct= await _cartRepository.GetCartProduct(requestDto.CartId,requestDto.ProductId);
            await _unitOfWork.BeginTransaction();
            try
            {
                targetCartProduct.Cart.TotalPrice -= targetCartProduct.UnitPrice;
                targetCartProduct.Product.QuantityInStock += targetCartProduct.Quantity;
                _cartProductRepository.Update(targetCartProduct);
                await _unitOfWork.SaveChangeAsync();
                _cartProductRepository.Delete(targetCartProduct);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            await _unitOfWork.CommitTransaciton();

            return new ResponseModel<object>(null, "Product Removed From Cart", true);


        }
    }
}
