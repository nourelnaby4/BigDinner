using BigDinner.Domain.Models.Menus;

namespace BigDinner.Persistence.Configurations.Menus;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.HasKey(item => item.Id);

        builder.Property(item => item.Name).IsRequired().HasMaxLength(150);

        builder.Property(item => item.Description).IsRequired().HasMaxLength(500);

        builder.HasMany(m => m.Items)
            .WithOne()
            .HasForeignKey(p => p.MenuId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
