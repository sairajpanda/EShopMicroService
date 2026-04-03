using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Infra.Data.Interceptors;

namespace Ordering.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("OrderingDb");

        services.AddScoped<ISaveChangesInterceptor, AudittableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp,options) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>();
            options.AddInterceptors(interceptors);
            options.UseSqlServer(connectionString);
        });


        return services;
    }
}