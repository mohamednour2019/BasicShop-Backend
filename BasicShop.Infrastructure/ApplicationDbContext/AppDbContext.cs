using BasicShop.Core.Domain.Entities;
using BasicShop.Core.ServiceInterfaces.ConfigurationsInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BasicShop.Infrastructure.ApplicationDbContext
{
    public class AppDbContext:IdentityDbContext<User,UserRole,Guid>
    {
        private IAppConfigurationService _appConfigurationService;

        public AppDbContext(IAppConfigurationService appConfigurationService)
        {
            _appConfigurationService = appConfigurationService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_appConfigurationService.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRole>().ToTable("UserRole");
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            builder.Ignore<IdentityUserClaim<Guid>>();
            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserRole<Guid>>();
        }
    }
}
