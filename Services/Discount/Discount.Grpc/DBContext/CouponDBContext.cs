using Microsoft.EntityFrameworkCore;
using Discount.Grpc.Models;
namespace Discount.Grpc.DBContext;

public class CouponDBContext : DbContext
{
    public CouponDBContext(DbContextOptions<CouponDBContext> options) : base(options)
    {
    }
    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon
            {
                Id = 1,
                ProductName = "IPhone X",
                Description = "IPhone Discount",
                Amount = 150
            },
            new Coupon
            {
                Id = 2,
                ProductName = "Samsung S10",
                Description = "Samsung Discount",
                Amount = 100
            }
        );
    }
}
