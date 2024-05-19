using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Orders;

namespace BigDinner.Persistence.Configurations.Orders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(item => item.Id);

        builder.Property(item => item.OrderNumber).IsRequired().HasMaxLength(150);


        builder.HasMany(m => m.Items)
            .WithOne()
            .HasForeignKey(p => p.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x=>x.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
