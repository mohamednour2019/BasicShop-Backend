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
    public class ChangeProductQuantityService : IChangeProductQuantityService
    {
        private IGenericRepository<Product> _repository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ChangeProductQuantityService(IGenericRepository<Product> repository
            , IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ProductResponseDto>> perform(ChangeProductQuantityRequestDto? requestDto)
        {
            Product targetProduct = await _repository.GetByIdAsync(requestDto.ProductId);
            if (targetProduct is not null)
            {
                targetProduct.QuantityInStock = requestDto.Quantity;
                _repository.Update(targetProduct);
                await _unitOfWork.SaveChangeAsync();
                var response = _mapper.Map<ProductResponseDto>(targetProduct);
                return new ResponseModel<ProductResponseDto>(response, "Product Quantity Changed", true);
            }
            else
            {
                throw new NotFoundException("Product Not Found");
            }
        }
    }
}
