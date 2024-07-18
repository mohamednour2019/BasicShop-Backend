using BasicShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Infrastructure.EntitiesConfigurations
{
    public class CartProductConfigurations : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.ToTable("CartProduct");
            builder.Property(x => x.ProductId).ValueGeneratedNever();
            builder.Property(x=>x.CartId).ValueGeneratedNever();
            builder.HasKey(x => new { x.ProductId, x.CartId });

            builder.HasOne(x=>x.Cart).WithMany(x=>x.CartProducts).HasForeignKey(x=>x.CartId);
            builder.HasOne(x=>x.Product).WithOne(x=>x.CartProduct).HasForeignKey<CartProduct>(x=>x.ProductId);
        }
    }
}
