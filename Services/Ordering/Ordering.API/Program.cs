using Ordering.API;
using Ordering.Application;
using Ordering.Infra;


var builder = WebApplication.CreateBuilder(args);

//Add Services to the Container
builder.Services
    .AddApplicationServices()
    .AddInfraServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


//Config the HTTP Request pipeline
app.Run();
