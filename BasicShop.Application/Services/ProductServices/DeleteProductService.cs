using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Core.ServiceInterfaces.ProductInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Application.Services.ProductServices
{
    public class DeleteProductService : IDeleteProductService
    {
        private IGenericRepository<Product> _repository;
        private IUnitOfWork _unitOfWork;

        public DeleteProductService(IGenericRepository<Product> repository,IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<object>> perform(Guid requestDto)
        {
            try
            {
                Product? targetProduct = await _repository.GetByIdAsync(requestDto);
                if(targetProduct is not null)
                {
                    _repository.Delete(targetProduct);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel<object>(null, "Product Deleted Successfully", true);
        }
    }
}
