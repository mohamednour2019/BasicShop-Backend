using BasicShop.Application.Services.ConfigurationServices;
using BasicShop.Core.Domain.Entities;
using BasicShop.Core.ServiceInterfaces.ConfigurationsInterfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BasicShop.Infrastructure.ApplicationDbContext;
using BasicShop.Infrastructure.Mapper;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore.Internal;
using BasicShop.Core.ServiceInterfaces.UserServicesInterfaces;
using BasicShop.Application.Services.UserServices;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Infrastructure.Repositories;
using BasicShop.Core.ServiceInterfaces.ProductInterfaces;
using BasicShop.Application.Services.ProductServices;

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
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAddProductService, AddProductService>();
            services.AddScoped<IDeleteProductService, DeleteProductService>();  
            services.AddScoped<IGetActiveProductsService, GetActiveProductsService>();
            services.AddScoped<IGetProductsService, GetProductsService>();
            services.AddScoped<IToggleProductStatusService, ToggleProductStatusService>();
            services.AddScoped<IChangeProductQuantityService, ChangeProductQuantityService>();
            return services;
        }


    }
}
