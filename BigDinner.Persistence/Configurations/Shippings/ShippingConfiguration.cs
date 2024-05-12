using BigDinner.Domain.Models.Orders;
using BigDinner.Domain.Models.Shippings;
using Microsoft.EntityFrameworkCore;

namespace BigDinner.Persistence.Configurations.Shippings;

public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
{
    public void Configure(EntityTypeBuilder<Shipping> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderId).IsRequired();

        builder.HasIndex(x => x.OrderId).IsUnique();

        builder.HasIndex(x => x.TrackingNumber).IsUnique();

        builder.HasOne(s => s.Order)
               .WithOne(o => o.Shipping)
               .HasForeignKey<Shipping>(s => s.OrderId);

        //builder.HasOne(s => s.Address)
        //       .WithOne(a => a.Shipping)
        //       .HasForeignKey<ShippingAddress>(sa => sa.ShippingId)
        //       .IsRequired()
        //       .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(x => x.Address);
    }
}
