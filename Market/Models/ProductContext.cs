using Microsoft.EntityFrameworkCore;

namespace Market.Models;

public class ProductContext : DbContext
{
    public DbSet<ProductStorage> ProductStorages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);
        }
    }

    // TODO:01:44:00
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Конфигурация Product
        modelBuilder.Entity<Product>()
            .ToTable("product")
            .Property(p => p.Cost)
            .IsRequired(); // Стоимость обязательна

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category) // Одна категория
            .WithMany(c => c.Products) // Со многими продуктами
            .HasForeignKey(p => p.CategoryId); // Внешний ключ

        // Конфигурация Category
        modelBuilder.Entity<Category>()
            .ToTable("category")
            .HasMany(c => c.Products) // Множество продуктов
            .WithOne(p => p.Category) // С одной категорией
            .HasForeignKey(p => p.CategoryId); // Внешний ключ

        // Конфигурация ProductStorage
        modelBuilder.Entity<ProductStorage>()
            .ToTable("product_storage")
            .HasKey(ps => new { ps.ProductId, ps.StorageId }); // Композитный ключ

        modelBuilder.Entity<ProductStorage>()
            .HasOne(ps => ps.Product)
            .WithMany(p => p.ProductStorages)
            .HasForeignKey(ps => ps.ProductId);

        modelBuilder.Entity<ProductStorage>()
            .HasOne(ps => ps.Storage)
            .WithMany(s => s.ProductStorages)
            .HasForeignKey(ps => ps.StorageId);

        // Конфигурация Storage
        modelBuilder.Entity<Storage>()
            .ToTable("storage")
            .HasMany(s => s.ProductStorages)
            .WithOne(ps => ps.Storage)
            .HasForeignKey(ps => ps.StorageId);
    }
}