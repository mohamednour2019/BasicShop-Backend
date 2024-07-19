using BasicShop.Application.Services.ProductServices;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.DTO_S.Product.RequestDTOs;
using BasicShop.Core.DTO_S.Product.ResponseDTOs;
using BasicShop.Core.DTO_S.User.RequestDTOs;
using BasicShop.Core.DTO_S.User.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.ProductInterfaces;
using BasicShop.Core.ServiceInterfaces.UserServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasicShop.Presentation.API.Controllers
{
    public class ProductController:BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProductResponseDto>))]
        public async Task<IActionResult> add(AddProductRequestDto requestDTO
            , [FromServices] IAddProductService registereServices)
            => await presenter.Handle(requestDTO, registereServices);


        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<>))]
        public async Task<IActionResult> delete(Guid productId
            , [FromServices] IDeleteProductService deleteProductService)
        => await presenter.Handle(productId, deleteProductService);

        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ProductResponseDto>>))]
        public async Task<IActionResult> getActive([FromServices] IGetActiveProductsService getActiveProductsService)
        => await presenter.Handle(null, getActiveProductsService);



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<ProductResponseDto>>))]
        public async Task<IActionResult> getAll([FromServices] IGetProductsService getProductsService)
        => await presenter.Handle(null, getProductsService);

        [HttpPatch("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProductResponseDto>))]
        public async Task<IActionResult> toggleStatus(Guid productId
            ,[FromServices] IToggleProductStatusService toggleProductStatusService)
        => await presenter.Handle(productId, toggleProductStatusService);



        [HttpPatch("quantity")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<ProductResponseDto>))]
        public async Task<IActionResult> changeQuantity(ChangeProductQuantityRequestDto requestDto
            , [FromServices] IChangeProductQuantityService changeProductQuantityService)
        => await presenter.Handle(requestDto, changeProductQuantityService);

    }
}
