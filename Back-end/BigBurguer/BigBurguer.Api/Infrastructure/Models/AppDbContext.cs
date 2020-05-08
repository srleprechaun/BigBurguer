using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace BigBurguer.Api.Infrastructure.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(p => p.Product)
                .WithMany(p => p.ProductIngredient)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(i => i.ProductIngredient)
                .HasForeignKey(pi => pi.IngredientId);
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<User> User { get; set; }
    }
}
