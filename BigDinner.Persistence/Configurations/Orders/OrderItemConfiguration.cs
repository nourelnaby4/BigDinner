using BigDinner.Domain.Models.Orders;

namespace BigDinner.Persistence.Configurations.Orders;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name).IsRequired().HasMaxLength(150);

        builder.Property(m => m.Quantity).IsRequired();

        builder.ComplexProperty(m => m.Price).IsRequired();
    }
}
