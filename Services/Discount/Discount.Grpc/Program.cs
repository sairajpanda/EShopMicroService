using Discount.Grpc.Services;
using Discount.Grpc.DBContext;
using System;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<CouponDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GRPCDb")));
//Application build
var app = builder.Build();

// Configure the HTTP request middleware pipeline.
app.UseMigration();
app.MapGrpcService<DiscountProtoNewService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. " +
"To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<CouponDBContext>();
//    db.Database.Migrate();
//}

//Application run
app.Run();
