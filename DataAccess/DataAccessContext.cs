using InvestmentOrdersAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace investmentOrders.DataAccess;

public class DataAccessContext : DbContext
{
    public DataAccessContext(DbContextOptions<DataAccessContext> options) : base(options) { }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .Property(o => o.Price)
            .HasColumnType("decimal(18,4)");

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,4)");
    }
}
