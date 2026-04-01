using Microsoft.Extensions.DependencyInjection;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        //builder.Services.AddApiServices();
        return app;
    }
}
