using kaiDeeMak.Models;
using Microsoft.EntityFrameworkCore;

namespace kaiDeeMak.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetils> OrderDetils { get; set; }
        public DbSet<Products> Products { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>()
                        .HasMany(t => t.Orders)
                        .WithOne(s => s.Customer)
                        .HasForeignKey(s => s.CustomerID)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Products>()
                        .HasMany(t => t.OrdersDetils)
                        .WithOne(s => s.Product)
                        .HasForeignKey(s => s.ProductID)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Orders>()
                        .HasMany(t => t.OrderDetils)
                        .WithOne(s => s.Order)
                        .HasForeignKey(s => s.OrderID)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
