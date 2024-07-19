using BasicShop.Core.Domain.Entities;
using BasicShop.Core.DTO_S.User.RequestDTOs;
using BasicShop.Core.DTO_S.User.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.UserServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace BasicShop.Presentation.API.Controllers
{
    public class UserController:BaseController
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(ResponseModel<UserResponseDto>))]
        public async Task<IActionResult> Register(RegisterRequestDto requestDTO
            , [FromServices]IRegisterService registereServices)
            =>await presenter.Handle(requestDTO, registereServices);


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<UserResponseDto>))]
        public async Task<IActionResult> Login(SignInRequestDto requestDTO
        , [FromServices] ISignInService signInService)
        => await presenter.Handle(requestDTO, signInService);
    }
}
