namespace Discount.Grpc.DBContext;
using Microsoft.EntityFrameworkCore;

public static class Extensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbcontext = scope.ServiceProvider.GetRequiredService<CouponDBContext>();
        dbcontext.Database.MigrateAsync();

        return app;
    }
}
