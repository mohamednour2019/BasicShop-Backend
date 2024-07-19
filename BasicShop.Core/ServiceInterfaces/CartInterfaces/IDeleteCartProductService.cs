using BasicShop.Core.Domain.Entities;
using BasicShop.Core.DTO_S.Cart.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.ServiceInterfaces.CartInterfaces
{
    public interface IDeleteCartProductService:IGenericService<DeleteCartProductRequestDto,ResponseModel<object>>
    {

    }
}
