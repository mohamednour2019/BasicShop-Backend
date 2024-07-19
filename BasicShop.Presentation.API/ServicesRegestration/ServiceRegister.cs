using BasicShop.Application.Services.ConfigurationServices;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.ServiceInterfaces.ConfigurationsInterfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using BasicShop.Infrastructure.ApplicationDbContext;
using BasicShop.Infrastructure.Mapper;

namespace BasicShop.Presentation.API.ServicesRegestration
{
    public static class ServiceRegister
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // EF Core and Identity registration
            services.AddDbContext<AppDbContext>();
            services.AddIdentity<User,UserRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddUserStore<UserStore<User, UserRole, AppDbContext, Guid>>()
            .AddRoleStore<RoleStore<UserRole, AppDbContext, Guid>>();

            //Mapper
            services.AddAutoMapper(typeof(Mapper));

            services.AddScoped<IAppConfigurationService, AppConfigurationService>();
            return services;
        }
    }
}
