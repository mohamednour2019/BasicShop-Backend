using AutoMapper;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Core.DTO_S.Product.RequestDTOs;
using BasicShop.Core.DTO_S.Product.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.ProductInterfaces;
using BasicShop.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Application.Services.ProductServices
{
    public class UpdateProductServie : IUpdateProductService
    {
        private IGenericRepository<Product> _repository;
        private ICartRepository _cartRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UpdateProductServie(IGenericRepository<Product> repository
            , IUnitOfWork unitOfWork, IMapper mapper, ICartRepository cartRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<ResponseModel<ProductResponseDto>> perform(UpdateProductRequestDto? requestDto)
        {
            Product targetProduct = await _repository.GetByIdAsync(requestDto.ProductId);
            decimal difference = requestDto.Price - targetProduct.Price;
            if (targetProduct is not null)
            {
                targetProduct.QuantityInStock = requestDto.Quantity;
                targetProduct.Price= requestDto.Price;
                _repository.Update(targetProduct);
                var response = _mapper.Map<ProductResponseDto>(targetProduct);
                await _cartRepository.UpdatePrice(targetProduct.Id, targetProduct.Price,difference);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseModel<ProductResponseDto>(response, "Product Updated", true);
            }
            else
            {
                throw new NotFoundException("Product Not Found");
            }
        }
    }
}
