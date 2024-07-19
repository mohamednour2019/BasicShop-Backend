﻿using BasicShop.Core.Domain.Entities;
using BasicShop.Core.DTO_S.Cart.RequestDTOs;
using BasicShop.Core.DTO_S.Cart.ResponseDTOs;
using BasicShop.Core.DTO_S.Product.RequestDTOs;
using BasicShop.Core.DTO_S.Product.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.CartInterfaces;
using BasicShop.Core.ServiceInterfaces.ProductInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasicShop.Presentation.API.Controllers
{
    public class CartController:BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProductCartResponsDto>))]
        public async Task<IActionResult> addProduct(AddProductToCartRequestDto requestDTO
            , [FromServices] IAddProductToCartService addProductToCartService)
            => await presenter.Handle(requestDTO, addProductToCartService);


        [HttpPost("quantity")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProductCartResponsDto>))]
        public async Task<IActionResult> changeQuantity(ChangeProductCartQuantityRequestDto requestDTO
        , [FromServices] IChangeProductCartQuantityService changeProductCartQuantity)
        => await presenter.Handle(requestDTO, changeProductCartQuantity);

    }
}
