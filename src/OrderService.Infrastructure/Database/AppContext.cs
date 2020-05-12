using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Customers;
using OrderService.Domain.Payments;
using OrderService.Domain.Products;

namespace OrderService.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<OutboxMessage> OutboxMessages { get; set; }

        //public DbSet<InternalCommand> InternalCommands { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}