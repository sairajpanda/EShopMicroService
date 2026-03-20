var builder = WebApplication.CreateBuilder(args);

//Add Services to the Container
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


//Config the HTTP Request pipeline
app.Run();
