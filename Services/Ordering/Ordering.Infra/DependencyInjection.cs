namespace Ordering.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("OrderingDb");
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));


        return services;
    }
}