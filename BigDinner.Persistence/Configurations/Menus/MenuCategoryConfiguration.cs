using BigDinner.Domain.Models.Menus;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BigDinner.Persistence.Configurations.Menus;

public class MenuCategoryConfiguration : IEntityTypeConfiguration<MenuCategory>
{
    public void Configure(EntityTypeBuilder<MenuCategory> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name).IsRequired().HasMaxLength(150);

        builder.Property(m => m.Description).IsRequired().HasMaxLength(500);

        builder.HasMany(m => m.Menus)
            .WithOne()
            .HasForeignKey(ms => ms.MenuCategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
