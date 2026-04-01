using Ordering.API;
using Ordering.Application;
using Ordering.Infra;
using Ordering.Infra.Data.Extentions;


var builder = WebApplication.CreateBuilder(args);

//Add Services to the Container
builder.Services
    .AddApplicationServices()
    .AddInfraServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();
//Config the HTTP Request pipeline
app.UseApiServices();
if (app.Environment.IsDevelopment())
{
    await app.IntialiseDatabaseAsync();
}
app.Run();
