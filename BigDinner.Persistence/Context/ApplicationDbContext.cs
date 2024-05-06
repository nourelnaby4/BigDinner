using BigDinner.Domain.Identities;
using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Menus;
using BigDinner.Domain.Models.Orders;
using BigDinner.Persistence.Configurations;
using BigDinner.Persistence.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BigDinner.Persistence.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    {
    }

    public DbSet<Menu> Menus { get; set; }

    public DbSet<OutboxMessage> OutboxMessage { get; set; } 

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }

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

