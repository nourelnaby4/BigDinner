using BigDinner.Domain.Models.Shippings;

namespace BigDinner.Persistence.Configurations.Shippings;

public class ShippingMethodConfiguration : IEntityTypeConfiguration<ShippingMethod>
{
    public void Configure(EntityTypeBuilder<ShippingMethod> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

        builder.Property(x => x.Description).HasMaxLength(255).IsRequired();

        builder.HasMany<Shipping>()
            .WithOne()
            .HasForeignKey(x => x.ShippingMethodId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
