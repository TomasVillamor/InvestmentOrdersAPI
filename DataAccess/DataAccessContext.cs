using InvestmentOrdersAPI.DataAccess.Models;

namespace investmentOrders.DataAccess;

public class DataAccessContext : DbContext
{
    public DataAccessContext(DbContextOptions<DataAccessContext> options) : base(options) { }
    public DbSet<Order> Orders { get; set; }
    public DbSet<AssetType> AssetTypes { get; set; }
    public DbSet<OrderState> OrderStates { get; set; }
    public DbSet<Asset> Assets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<AssetType>(entity =>
        {
            entity.HasMany(t => t.Assets)
                  .WithOne(a => a.AssetType)
                  .HasForeignKey(a => a.AssetTypeId);
        });

        modelBuilder.Entity<OrderState>(entity =>
        {
            entity.HasMany(e => e.Orders)
                  .WithOne(o => o.OrderState)
                  .HasForeignKey(o => o.OrderStateId);
        });

        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasMany(a => a.Orders)
                  .WithOne(o => o.Asset)
                  .HasForeignKey(o => o.AssetId);
            entity.Property(a => a.UnitPrice)
                  .HasColumnType("decimal(18,4)");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(o => o.Price)
                  .HasColumnType("decimal(18,4)");
            entity.Property(o => o.TotalAmount)
                  .HasColumnType("decimal(18,4)");
        });

    }
}
