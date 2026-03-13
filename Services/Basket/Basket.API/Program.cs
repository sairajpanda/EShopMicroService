using Basket.API.DBContext;
using BuildingBlocks.Behaviour;
using BuildingBlocks.Exception;
using BuildingBlocks.Logging;
using Basket.API.Data;
using System;


var builder = WebApplication.CreateBuilder(args);
// Add services to ther container

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCarter();

builder.Services.AddDbContext<BasketDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(LoggerBehaviour<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddTransient<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, BasketRepository>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BasketDbContext>();
    db.Database.Migrate();
}

//Config the request pipeline
app.MapCarter();
app.UseExceptionHandler();

app.Run();
