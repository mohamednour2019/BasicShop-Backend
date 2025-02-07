﻿using AutoMapper;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Core.DTO_S.Product.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.ProductInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Application.Services.ProductServices
{
    public class GetActiveProductsService : IGetActiveProductsService
    {
        private IMapper _mapper;
        private IProductRepository _repository;

        public GetActiveProductsService(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseModel<List<ProductResponseDto>>> perform(Guid requestDto)
        {
            List<Product>result= await _repository.GetActiveProducts(requestDto);
            if(result is not null && result.Count > 0)
            {
                var response = _mapper.Map<List<ProductResponseDto>>(result);
                return new ResponseModel<List<ProductResponseDto>>(response, null, true);
            }
            else
            {
                return new ResponseModel<List<ProductResponseDto>>(null, "There are no Products", true);
            }

        }
    }
}
