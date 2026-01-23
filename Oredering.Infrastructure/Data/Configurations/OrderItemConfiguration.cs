using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Oredering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(oi => oi.Id).
            HasConversion(
            orderItemId => orderItemId.Value,
            dbId => OrderItemId.Of(dbId)
            );

        builder.HasOne<Product>().WithMany().HasForeignKey(oi => oi.ProductId);

        builder.Property(a => a.Quantity)
            .IsRequired();

        builder.Property(a => a.Price).IsRequired();
    }
}
