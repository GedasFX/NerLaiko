using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NerLaiko.Models;

namespace NerLaiko.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemDiscount> ItemDiscounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Produce> Produces { get; set; }
        public DbSet<Refrigerator> Refrigerators { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ItemDiscount>()
                .HasKey(c => new { c.ItemId, c.DiscountId });
            builder.Entity<OrderItem>()
                .HasKey(c => new { c.ItemId, c.OrderId });
            builder.Entity<Produce>()
                .HasKey(c => new { c.ItemId, c.FridgeId });
            base.OnModelCreating(builder);
        }
    }
}
