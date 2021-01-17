using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Models;

namespace WebStore.Database
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStock> OrderStocks { get; set; }
        public DbSet<StockOnHold> StocksOnHold { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderStock>().HasKey(x => new { x.StockId, x.OrderId });
            builder.Entity<Product>().Property(p => p.MinValue).HasColumnType("decimal(18,2)");
            builder.Entity<Stock>().Property(p => p.StockValue).HasColumnType("decimal(18,2)");
        }
    }
}
