using EnternetShop.Models;
using Microsoft.EntityFrameworkCore;

namespace EnternetShop.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CartItem>()
            .HasKey(t => new { t.ProductId, t.CartId });

            builder.Entity<CartItem>()
                .HasOne(sc => sc.Product)
                .WithMany(s => s.CartItems)
                .HasForeignKey(sc => sc.ProductId);

            builder.Entity<CartItem>()
                .HasOne(sc => sc.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(sc => sc.CartId);

            builder.Entity<OrderItem>()
            .HasKey(t => new { t.ProductId, t.OrderId });

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}
