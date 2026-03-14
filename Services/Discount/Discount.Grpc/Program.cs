using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

//Application build
var app = builder.Build();

// Configure the HTTP request middleware pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. " +
"To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

//Application run
app.Run();
