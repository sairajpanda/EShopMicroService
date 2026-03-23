using Microsoft.Extensions.DependencyInjection;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        return services;
    }

    public static WebApplicationBuilder UseApiCservices(this WebApplicationBuilder builder)
    {
        //builder.Services.AddApiServices();
        return builder;
    }
}
