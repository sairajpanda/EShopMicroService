using Microsoft.Extensions.DependencyInjection;
using Carter;
using BuildingBlocks.Exception;
using HealthChecks;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,ConfigurationManager _config)
    {
        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHealthChecks().AddSqlServer(_config.GetConnectionString("OrderingDb")!);
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseExceptionHandler(options => { });
        app.UseHealthChecks("/health");
        return app;
    }
}
