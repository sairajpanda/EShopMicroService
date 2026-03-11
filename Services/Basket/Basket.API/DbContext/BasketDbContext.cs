using Microsoft.EntityFrameworkCore;
namespace Basket.API.DBContext;


public class BasketDbContext : DbContext
{
    public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
    {
    }
     public DbSet<Models.ShoppingCart> ShoppingCarts { get; set; }
}
