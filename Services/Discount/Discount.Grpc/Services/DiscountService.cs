using Discount.Grpc.DBContext;
using Discount.Grpc.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountProtoNewService (CouponDBContext couponDB) : DiscountService.DiscountServiceBase
{
    public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await couponDB.Coupons.AsNoTracking().Where(x => x.ProductName == request.ProductName).FirstOrDefaultAsync();
        return new CouponModel
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Amount = coupon.Amount,
            Description = coupon.Description
        };
    }

    public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        Coupon objCoupon = new Coupon
        {
            ProductName = request.Coupon.ProductName,
            Amount = request.Coupon.Amount,
            Description = request.Coupon.Description,
            Id = request.Coupon.Id
        };
        await couponDB.Coupons.AddAsync(objCoupon);
        await couponDB.SaveChangesAsync();
        return new CouponModel
        {
            Id = objCoupon.Id,
            ProductName = objCoupon.ProductName,
            Amount = objCoupon.Amount,
            Description = objCoupon.Description
        };
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var _coupons = couponDB.Coupons.Where(x => x.ProductName == request.Coupon.ProductName).FirstOrDefault()!;
        _coupons.ProductName = request.Coupon.ProductName;
        _coupons.Amount = request.Coupon.Amount;
        _coupons.Description = request.Coupon.Description;

        couponDB.Coupons.Update(_coupons);
        await couponDB.SaveChangesAsync();
        return new CouponModel
        {
            Id = request.Coupon.Id,
            ProductName = request.Coupon.ProductName,
            Amount = request.Coupon.Amount,
            Description = request.Coupon.Description
        };
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var _coupons = couponDB.Coupons.Where(x => x.ProductName == request.ProductName).FirstOrDefault()!;
        couponDB.Coupons.Remove(_coupons);
        await couponDB.SaveChangesAsync();
        return new DeleteDiscountResponse{
            Success = true
        };
    }
}
