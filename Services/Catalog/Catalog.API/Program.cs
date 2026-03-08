
using BuildingBlocks.Behaviour;
using BuildingBlocks.Exception;
using BuildingBlocks.Logging;
using Catalog.API.DBContext;
using System;

var builder = WebApplication.CreateBuilder(args);

//builder.AddServiceDefaults();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCarter();

builder.Services.AddDbContext<CatalogDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
    configuration.AddOpenBehavior(typeof(LoggerBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CatalogDBContext>();
    db.Database.Migrate();
}

//app.MapDefaultEndpoints();

app.MapCarter();
app.UseExceptionHandler();

app.Run();
