using BasicShop.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.ServiceInterfaces.ProductInterfaces
{
    public interface IDeleteProductService:IGenericService<Guid,ResponseModel<object>>
    {
    }
}
