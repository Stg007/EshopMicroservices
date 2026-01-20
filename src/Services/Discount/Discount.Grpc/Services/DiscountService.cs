using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbcontext, ILogger<DiscountService> logger) 
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        Coupon? coupon = dbcontext.Coupons
            .FirstOrDefault(c => c.ProductName == request.ProductName);

        if (coupon == null)
        {
            coupon = new Coupon
            {
                ProductName = request.ProductName,
                Description = "No Discount",
                Amount = 0
            };
        }

        var couponModel = coupon.Adapt<CouponModel>();  

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon = request.Adapt<Coupon>();

        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
        }

        dbcontext.Coupons.Add(coupon);
        await dbcontext.SaveChangesAsync();

        CouponModel result = coupon.Adapt<CouponModel>();

        return result;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        Coupon? old_coupon = dbcontext.Coupons.FirstOrDefault(c=>c.ProductName == request.Coupon.ProductName);

        if (old_coupon == null) {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
        }

        old_coupon.Amount = (decimal)request.Coupon.Amount;
        old_coupon.Description = request.Coupon.Description;

        dbcontext.Coupons.Update(old_coupon);
        await dbcontext.SaveChangesAsync();

        CouponModel result = old_coupon.Adapt<CouponModel>();
        return result;



    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {

        Coupon? coupon = dbcontext.Coupons
            .FirstOrDefault(c => c.ProductName == request.ProductName);

        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
        }

        dbcontext.Coupons.Remove(coupon);
        await dbcontext.SaveChangesAsync();

        return new DeleteDiscountResponse { Success = true };
    }
}
