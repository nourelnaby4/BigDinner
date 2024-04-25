using BigDinner.Domain.Models.Menus.Aggregates;
using BigDinner.Domain.Models.Menus.Entities;
using BigDinner.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Persistence.Configurations
{
    public static class MenuConfiguration
    {
        public static ModelBuilder AddMenuConfiguration(this ModelBuilder modelBuilder)
        {
            return modelBuilder
                .ConfigureMenus()
                .ConfigureMenuItems();
        }
        public static ModelBuilder ConfigureMenus(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(menu =>
            {
                menu.ToTable("Menus");
                menu.HasKey(m => m.Id);
                menu.Property(m => m.Name).IsRequired();
                menu.Property(m => m.Description).IsRequired();
                menu.HasMany(m => m.Items)
                    .WithOne()
                    .HasForeignKey(item => item.Id)
                    .OnDelete(DeleteBehavior.Cascade);
                menu.HasIndex(m => m.Name).IsUnique();
            });

            return modelBuilder;
        }

        public static ModelBuilder ConfigureMenuItems(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>()
                .HasMany(m => m.Items)
                .WithOne(item => item.Menu)
                .HasForeignKey(item => item.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuItem>(menuItem =>
            {
                menuItem.ToTable("MenuItems");
                menuItem.HasKey(item => item.Id);
                menuItem.Property(item => item.Name).IsRequired();
                menuItem.Property(item => item.Description).IsRequired();
                menuItem.OwnsOne(item => item.Price, price =>
                {
                    price.Property(p => p.Value).HasColumnName(nameof(Price));
                    price.Property(p => p.Currency).HasColumnName(nameof(Price.Currency)); 
                });
            });
            return modelBuilder;
        }
    }
}

