using Microsoft.EntityFrameworkCore;
using Discount.Grpc.Models;
namespace Discount.Grpc.DBContext;

public class CouponDBContext : DbContext
{
    public CouponDBContext(DbContextOptions<CouponDBContext> options) : base(options)
    {
    }
    public DbSet<Coupon> Coupons { get; set; }
}
