using BasicShop.Presentation.API.ServicesRegestration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder.Configuration);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
