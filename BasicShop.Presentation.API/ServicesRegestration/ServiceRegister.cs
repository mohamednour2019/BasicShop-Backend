using BasicShop.Application.Services.ConfigurationServices;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.ServiceInterfaces.ConfigurationsInterfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BasicShop.Infrastructure.ApplicationDbContext;
using BasicShop.Infrastructure.Mapper;
using Asp.Versioning;

namespace BasicShop.Presentation.API.ServicesRegestration
{
    public static class ServiceRegister
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers();
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


            // Swagger configuration registration
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Basic Shop API v1",
                    Version = "v1"
                });
            });

            // API versioning registration
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });



            services.AddScoped<IAppConfigurationService, AppConfigurationService>();
            return services;
        }
    }
}
