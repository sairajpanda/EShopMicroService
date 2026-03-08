using BuildingBlocks.Behaviour;
using BuildingBlocks.Exception;
using BuildingBlocks.Logging;
using System;


var builder = WebApplication.CreateBuilder(args);
// Add services to ther container
builder.Services.AddCarter();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(LoggerBehaviour<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});


var app = builder.Build();


//Config the request pipeline
app.MapCarter();


app.Run();
