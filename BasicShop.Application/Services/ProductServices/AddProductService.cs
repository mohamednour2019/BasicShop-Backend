using AutoMapper;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Core.DTO_S.Product.RequestDTOs;
using BasicShop.Core.DTO_S.Product.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.ProductInterfaces;
using BasicShop.Shared.CustomExceptions;

namespace BasicShop.Application.Services.ProductServices
{
    public class AddProductService : IAddProductService
    {
        private IMapper _mapper;
        private IGenericRepository<Product> _repository;
        private IUnitOfWork _unitOfWork;

        public AddProductService(IMapper mapper, IGenericRepository<Product> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<ProductResponseDto>> perform(AddProductRequestDto requestDto)
        {
            Product newProduct = _mapper.Map<Product>(requestDto);
            newProduct.Id = Guid.NewGuid();
            newProduct.IsActive=true;

            bool isResident=await _repository.IsResident(x=>x.Name==newProduct.Name);
            if (isResident)
            {
                throw new ViolenceConstraintException("Product Already Exists");
            }
            try
            {
                await _repository.AddAsync(newProduct);
            }catch(Exception ex)
            {
                throw new ViolenceConstraintException(ex.Message);
            }
            await _unitOfWork.SaveChangeAsync();

            ProductResponseDto response = _mapper.Map<ProductResponseDto>(newProduct);
            return new ResponseModel<ProductResponseDto>(response, "Prodcut has been added Successfully", true);


        }
    }
}
