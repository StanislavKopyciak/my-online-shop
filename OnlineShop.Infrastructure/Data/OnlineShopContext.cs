using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Entities;

namespace OnlineShop.Infrastructure.Data
{
    public class OnlineShopContext : DbContext
    {
        public OnlineShopContext(DbContextOptions<OnlineShopContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineShopContext).Assembly);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineShopContext).Assembly);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Id)
                .IsUnique();

            modelBuilder.Entity<Address>()
                .HasIndex(u => u.Id)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(u => u.Id)
                .IsUnique();


            modelBuilder.Entity<Purchase>()
                .HasIndex(u => u.Id)
                .IsUnique();

            base.OnModelCreating(modelBuilder);

        }
    }
}
