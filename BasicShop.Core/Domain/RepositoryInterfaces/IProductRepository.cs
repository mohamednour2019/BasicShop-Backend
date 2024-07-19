using BasicShop.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.Domain.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetActiveProducts(Guid cartId);
    }
}
