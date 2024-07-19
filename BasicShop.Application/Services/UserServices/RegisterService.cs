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
        private IMapper _mapper;
        private ISignInService _signInService;
        private IGenericRepository<Cart> _cartRepository;
        private IUnitOfWork _unitOfWork;


        public RegisterService(UserManager<User> userManager,IMapper mapper,
            ISignInService signInService, IGenericRepository<Cart> cartRepository
            , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _signInService = signInService;
            _userManager = userManager;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
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
                //add cart for user
                Cart cart = new Cart()
                {
                    Id = Guid.NewGuid(),
                    TotalPrice = 0,
                    UserId = user.Id
                };
                await _cartRepository.AddAsync(cart);
                await _unitOfWork.SaveChangeAsync();



                //sign the user in
                SignInRequestDto signInRequestDto = new SignInRequestDto()
                {
                    Email = requestDto.Email,
                    Password = requestDto.Password
                };
                var signInResult = await _signInService.perform(signInRequestDto);
                RegisterResponseDto respons = _mapper.Map<RegisterResponseDto>(signInResult.Data);
                return new ResponseModel<RegisterResponseDto>(respons,"Welcome", true);
            }


        }
    }
}
