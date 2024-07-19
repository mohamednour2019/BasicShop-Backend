using AutoMapper;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.DTO_S.User.RequestDTOs;
using BasicShop.Core.DTO_S.User.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.UserServicesInterfaces;
using BasicShop.Shared.CustomExceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Application.Services.UserServices
{
    public class SignInService : ISignInService
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IMapper _mapper;

        public SignInService(UserManager<User> userManager, SignInManager<User> signInManager
            , IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<ResponseModel<SignInResponseDto>> perform(SignInRequestDto requestDto)
        {
            User? user= await _userManager.FindByEmailAsync(requestDto.Email);
            if(user is not null)
            {
              SignInResult result=  await _signInManager.PasswordSignInAsync(requestDto.Email
                  , requestDto.Password,false,false);
                if (!result.Succeeded)
                {
                    throw new ViolenceConstraintException("Wrong Password");
                }
                else
                {
                    SignInResponseDto response= _mapper.Map<SignInResponseDto>(user);
                    return new ResponseModel<SignInResponseDto>(response, "Welcome!", true);
                }

            }
            else
            {
                throw new NotFoundException("User Not Registered!");
            }
        }
    }
}
