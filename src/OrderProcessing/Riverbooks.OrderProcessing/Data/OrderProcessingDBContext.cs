using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Riverbooks.OrderProcessing.Data
{
    internal class OrderProcessingDBContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public OrderProcessingDBContext(DbContextOptions<OrderProcessingDBContext> options) : base(options)
        {
        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("OrderProcessing");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
        }
    }
}