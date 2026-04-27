using Ordering.API;
using Ordering.Application;
using Ordering.Infra;
using Ordering.Infra.Data.Extentions;


var builder = WebApplication.CreateBuilder(args);

//Add Services to the Container
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfraServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();
//Config the HTTP Request pipeline
app.UseApiServices();
if (app.Environment.IsDevelopment())
{
    await app.IntialiseDatabaseAsync();
}
app.Run();
