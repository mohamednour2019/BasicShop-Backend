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
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            // If you don't need roles, you can remove them from the model
            builder.Entity<IdentityRole<Guid>>().ToTable("Roles").HasKey(r => r.Id);
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        }
    }
}
