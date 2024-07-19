using BasicShop.Presentation.API;
using BasicShop.Presentation.API.ServicesRegestration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder.Configuration);
var app = builder.Build();
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

app.Run();
