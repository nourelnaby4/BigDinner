using BigDinner.Domain.Models.Menus;
namespace BigDinner.Persistence.Configurations.Menus;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name).IsRequired().HasMaxLength(150);

        builder.Property(m => m.Description).IsRequired().HasMaxLength(500);

        builder.ComplexProperty(m => m.Price).IsRequired();
    }
}
