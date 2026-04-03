using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infra.Data.Extentions;

public static class DatabaseExtentions
{
    public static async Task IntialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
       
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCutomerAsync(context);
        await SeedProductAsync(context);
        await SeedPOrderandItemAsync(context);
    }
    private static async Task SeedCutomerAsync (ApplicationDbContext context)
    {
        if (! await context.Customers.AnyAsync())
        {
            await context.Customers.AddRangeAsync(InitialData.Customers);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedProductAsync(ApplicationDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(InitialData.Products);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedPOrderandItemAsync(ApplicationDbContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(InitialData.OrderWithItems);
            await context.SaveChangesAsync();
        }
    }
}
