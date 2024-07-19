using BasicShop.Core.Domain.Entities;
using BasicShop.Core.DTO_S.Product.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.ServiceInterfaces.ProductInterfaces
{
    public interface IToggleProductStatusService:IGenericService<Guid,ResponseModel<ProductResponseDto>>
    {
    }
}
