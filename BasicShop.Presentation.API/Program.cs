using BasicShop.Presentation.API;
using BasicShop.Presentation.API.ServicesRegestration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder.Configuration);
var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var userManager = services.GetRequiredService<UserManager<User>>();

//    // Call the seed method
//    await SeedAdminData.Initialize(userManager);
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
