using AutoMapper;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Core.DTO_S.Cart.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.CartInterfaces;
using BasicShop.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Application.Services.CartServices
{
    public class GetCartProductsService : IGetCartProductsService
    {
        private ICartRepository _cartRepository;
        private IMapper _mapper;

        public GetCartProductsService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ProductCartResponsDto>>> perform(Guid requestDto)
        {
            List<CartProduct> result = await _cartRepository.GetCartProducts(requestDto);
            if(result is not null&&result.Count()>0)
            {
                var response= _mapper.Map<List<ProductCartResponsDto>>(result);
                return new ResponseModel<List<ProductCartResponsDto>>(response, null, true);
            }
            else
            {
                throw new NotFoundException("no data in cart");
            }
        }
    }
}
