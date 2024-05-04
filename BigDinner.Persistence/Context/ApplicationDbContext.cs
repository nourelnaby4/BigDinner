using BigDinner.Domain.Identities;
using BigDinner.Domain.Models.Menus;
using BigDinner.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BigDinner.Persistence.Context;

public class DinnerDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public DinnerDbContext(DbContextOptions<DinnerDbContext> options)
      : base(options)
    {
    }

    public DbSet<Menu> Menus { get; set; }

    public DbSet<MenuItem> MenuItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());// apply Entities Configurations
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}

