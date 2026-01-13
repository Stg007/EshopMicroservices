using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DbSet<Models.Coupon> Coupons { get; set; } = default!;

    public DiscountContext(DbContextOptions<DiscountContext> options):base(options)
    {

        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Coupon>().HasData(
            new Models.Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 10 } ,
            new Models.Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 12 }
            );
    }

}
