using AutoMapper;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Core.DTO_S.User.RequestDTOs;
using BasicShop.Core.DTO_S.User.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.UserServicesInterfaces;
using BasicShop.Shared.CustomExceptions;
using Microsoft.AspNetCore.Identity;

namespace BasicShop.Application.Services.UserServices
{
    public class RegisterService : IRegisterService
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IMapper _mapper;
        private ISignInService _signInService;  


        public RegisterService(UserManager<User> _userManager,IMapper mapper
            , SignInManager<User>signInManager,ISignInService signInService)
        {
            _mapper=mapper;
            _signInManager=signInManager;
            _signInService=signInService;
        }

        public async Task<ResponseModel<RegisterResponseDto>> perform(RegisterRequestDto requestDto)
        {
            User user=_mapper.Map<User>(requestDto);
            user.Id = Guid.NewGuid();
            user.Role = "Client";

            IdentityResult result= await _userManager.CreateAsync(user,requestDto.Password);
            if (!result.Succeeded)
            {
                throw new ViolenceConstraintException(string.Join('\n', result.Errors.Select(x => x.Description)));
            }
            else
            {
                SignInRequestDto signInRequestDto = new SignInRequestDto()
                {
                    Email = requestDto.Email,
                    Password = requestDto.Password
                };
                var signInResult = await _signInService.perform(signInRequestDto);
                RegisterResponseDto respons = _mapper.Map<RegisterResponseDto>(signInResult.Data);
                return new ResponseModel<RegisterResponseDto>(respons,"Welcom", true);
            }


        }
    }
}
