using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Infrastructure.SeedData;
using BasicShop.Presentation.API;
using BasicShop.Presentation.API.ServicesRegestration;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder.Configuration);
var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var userManager = services.GetRequiredService<UserManager<User>>();
//    var cartRepository = services.GetRequiredService<IGenericRepository<Cart>>();
//    var unitOfWork = services.GetRequiredService<IUnitOfWork>();

//    await SeedAdminData.Initialize(userManager,cartRepository,unitOfWork);
//}
app.UseGlobalExceptionMiddleware();
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});
//app.UseAuthentication();
//app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basic Shop API v1");
});

app.Run();
