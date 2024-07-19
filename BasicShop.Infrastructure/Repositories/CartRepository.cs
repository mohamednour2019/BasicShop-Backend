using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private DbSet<CartProduct> _entity;

        public CartRepository(AppDbContext appDbContext)
        {
            _entity = appDbContext.Set<CartProduct>();
        }
        public async Task<CartProduct> GetCartProduct(Guid CartId, Guid ProductId)
        {
            return await _entity
                .Include(x => x.Cart)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.CartId == CartId && x.ProductId == ProductId);
        }

        public async Task<List<CartProduct>> GetCartProducts(Guid CartId)
        {
            return await _entity.Include(x=>x.Product)
                .Include(x=>x.Cart)
                .Where(x=>x.CartId==CartId)
                .ToListAsync();
        }
    }
}
