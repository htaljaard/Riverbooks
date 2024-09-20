using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Riverbooks.OrderProcessing;

namespace RiverBooks.OrderProcessing;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.ComplexProperty(p => p.ShippingAddress, address =>
        {
            address.Property(a => a.Street1).HasMaxLength(100);
            address.Property(a => a.Street2).HasMaxLength(100);
            address.Property(a => a.Country).HasMaxLength(50);
            address.Property(a => a.City).HasMaxLength(50);
            address.Property(a => a.State).HasMaxLength(50);
            address.Property(a => a.PostalCode).HasMaxLength(10);
        });

        builder.ComplexProperty(p => p.BillingAddress, address =>
        {
            address.Property(a => a.Street1).HasMaxLength(100);
            address.Property(a => a.Street2).HasMaxLength(100);
            address.Property(a => a.Country).HasMaxLength(50);
            address.Property(a => a.City).HasMaxLength(50);
            address.Property(a => a.State).HasMaxLength(50);
            address.Property(a => a.PostalCode).HasMaxLength(10);
        });
    }
}

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    void IEntityTypeConfiguration<OrderItem>.Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Description).HasMaxLength(100);
    }
}

