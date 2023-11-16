using December.Core.Entities;
using December.Core.Entities.Areas;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace December.DataAccess.ModelConfiguration;

public static class ModelBuilderExtensions
{
    public static void ConfigureUniqueConstraints(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>()
            .HasIndex(p => p.BrandName)
            .IsUnique();
        modelBuilder.Entity<Category>()
            .HasIndex(p => p.CategoryName)
            .IsUnique();
        modelBuilder.Entity<Collection>()
            .HasIndex(p => p.CollectionName)
            .IsUnique();
        modelBuilder.Entity<Color>()
            .HasIndex(p => p.ColorName)
            .IsUnique();
        modelBuilder.Entity<Size>()
            .HasIndex(p => p.Type)
            .IsUnique();
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(10, 2)");
        modelBuilder.Entity<Product>()
            .Property(p => p.Discount)
            .HasColumnType("decimal(10, 2)")
            .IsRequired();
        modelBuilder.Entity<Basket>()
            .Property(b => b.Price)
            .HasColumnType("decimal(18,2)");
    }
}

