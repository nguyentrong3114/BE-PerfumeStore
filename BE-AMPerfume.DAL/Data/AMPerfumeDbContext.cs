using Microsoft.EntityFrameworkCore;
using BE_AMPerfume.Core.Models;

namespace BE_AMPerfume.DAL.Data;

public class AMPerfumeDbContext : DbContext
{
    public AMPerfumeDbContext(DbContextOptions<AMPerfumeDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<Brand> Brands => Set<Brand>();

    // Nếu có thêm:
    // public DbSet<Category> Categories => Set<Category>();
    // public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Cấu hình quan hệ 1 - N: Product -> ProductVariants
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Variants)
            .WithOne(v => v.Product)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductImage)
            .WithMany() // hoặc .WithOne() nếu 1-1
            .HasForeignKey(p => p.ProductImageId)
            .HasPrincipalKey(pi => pi.Id);


    }
}
