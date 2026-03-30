
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.ValueObjects;
using Ordering.Domain.Models;


namespace Ordering.Infra.Configuration;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            customerid => customerid.Value,
            dbId => CustomerId.Of(dbId)
        );
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(c => c.Email).IsUnique();
    }
}
