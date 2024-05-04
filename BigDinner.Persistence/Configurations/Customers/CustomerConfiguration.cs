using BigDinner.Domain.Models.Customers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigDinner.Persistence.Configurations.Customers;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);

        builder.Property(c => c.Phone).IsRequired().HasMaxLength(15);

        builder.ComplexProperty(c => c.Email)
            .Property(e=>e.Value)
            .IsRequired()
            .HasMaxLength(150);

        builder.ComplexProperty(c => c.Address);
    }
}

