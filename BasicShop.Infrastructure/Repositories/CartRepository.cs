using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private DbSet<CartProduct> _entity;
        private DbSet<Cart> _cartEntity;

        public CartRepository(AppDbContext appDbContext)
        {
            _entity = appDbContext.Set<CartProduct>();
            _cartEntity = appDbContext.Set<Cart>();
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

        public async Task UpdatePrice(Guid ProductId, decimal newPrice,decimal priceDifference)
        {
            // Step 1: Update the UnitPrice of the Product
            await _entity.Where(x => x.ProductId == ProductId)
                         .ExecuteUpdateAsync(setters => setters
                             .SetProperty(x => x.UnitPrice, x=>x.Quantity*newPrice));

            // Step 2: Retrieve the necessary product information (Quantity and CartId)
            var productInfo = await _entity
                                  .Where(x => x.ProductId == ProductId)
                                  .Select(x => new { x.Quantity, x.CartId })
                                  .FirstOrDefaultAsync();

            if (productInfo != null && productInfo.CartId != null)
            {
                // Step 3: Update the TotalPrice of the Cart
                await _cartEntity
                              .Where(c => c.Id == productInfo.CartId)
                              .ExecuteUpdateAsync(setters => setters
                                  .SetProperty(c => c.TotalPrice, c => c.TotalPrice + (priceDifference * productInfo.Quantity)));
            }
        }
    }
}
