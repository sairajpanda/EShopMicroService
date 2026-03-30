using Microsoft.EntityFrameworkCore;
using Ordering.Domain.ValueObjects;
using Ordering.Domain.Models;

namespace Ordering.Infra.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).HasConversion(
            id => id.Value,
            dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
          .WithOne()
          .HasForeignKey(o => o.OrderId);

        builder.ComplexProperty(o => o.OrderName, nameBuillder =>
        {
            nameBuillder.Property(n => n.Value)
              .HasColumnName("OrderName")
              .HasMaxLength(100)
              .IsRequired();
        });

        builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.Street)
              .HasColumnName("BillingStreet")
              .HasMaxLength(200)
              .IsRequired();
            addressBuilder.Property(a => a.City)
              .HasColumnName("BillingCity")
              .HasMaxLength(100)
              .IsRequired();
            addressBuilder.Property(a => a.State)
              .HasColumnName("BillingState")
              .HasMaxLength(50)
              .IsRequired();
            addressBuilder.Property(a => a.ZipCode)
              .HasColumnName("BillingZipCode")
              .HasMaxLength(20)
              .IsRequired();
        });

        builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.Street)
              .HasColumnName("ShippingStreet")
              .HasMaxLength(200)
              .IsRequired();
            addressBuilder.Property(a => a.City)
              .HasColumnName("ShippingCity")
              .HasMaxLength(100)
              .IsRequired();
            addressBuilder.Property(a => a.State)
              .HasColumnName("ShippingState")
              .HasMaxLength(50)
              .IsRequired();
            addressBuilder.Property(a => a.ZipCode)
              .HasColumnName("ShippingZipCode")
              .HasMaxLength(20)
              .IsRequired();
        });

        builder.ComplexProperty(o => o.payment, paymentBuilder =>
        {
            paymentBuilder.Property(p => p.CardName)
              .HasColumnName("PaymentCardName")
              .HasMaxLength(50)
              .IsRequired();
            paymentBuilder.Property(p => p.CardNumber)
              .HasColumnName("PaymentCardNumber")
              .HasPrecision(18, 2)
              .IsRequired();
            paymentBuilder.Property(p => p.CardHolderName)
             .HasColumnName("PaymentCardHolderName")
             .HasMaxLength(18)
             .IsRequired();
            paymentBuilder.Property(p => p.CVV)
            .HasColumnName("PaymentCVV")
            .HasMaxLength(18)
            .IsRequired();
        });

        builder.Property(o => o.TotalPrice);

    }
}
