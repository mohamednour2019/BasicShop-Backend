using BasicShop.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.Domain.RepositoryInterfaces
{
    public interface ICartRepository
    {
        Task<CartProduct> GetCartProduct(Guid CartId, Guid ProductId);
        Task<List<CartProduct>> GetCartProducts(Guid CartId);
    }
}
