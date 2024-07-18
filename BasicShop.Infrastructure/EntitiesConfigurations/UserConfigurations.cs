using BasicShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Infrastructure.EntitiesConfigurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.SecurityStamp);
            builder.Ignore(x => x.ConcurrencyStamp);
            builder.Ignore(x => x.TwoFactorEnabled);
            builder.Ignore(x => x.LockoutEnd);
            builder.Ignore(x => x.LockoutEnabled);
            builder.Ignore(x => x.AccessFailedCount);
        }
    }
}
