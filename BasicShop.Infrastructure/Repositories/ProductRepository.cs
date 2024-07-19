using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Core.DTO_S.Product.ResponseDTOs;
using BasicShop.Core.ServiceInterfaces.ProductInterfaces;
using BasicShop.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DbSet<Product> _entity;

        public ProductRepository(AppDbContext appDbContext)
        {
            _entity = appDbContext.Set<Product>();
        }

        public async Task<List<Product>> GetActiveProducts(Guid cartId)
        {
            return await _entity.Include(x=>x.CartProduct).Where(x=>x.CartProduct.CartId!=cartId&&x.IsActive).ToListAsync();
        }

    }
}
