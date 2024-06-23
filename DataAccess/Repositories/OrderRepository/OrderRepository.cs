using investmentOrders.DataAccess;
using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.DataAccess.Repositories.GenericRepository;


namespace InvestmentOrdersAPI.DataAccess.Repositories.OrderRepository.OrderRepository;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(DataAccessContext context) : base(context) { }

    public async Task<IEnumerable<Order>> GetAllOrdersWithRelationsAsync()
    {
        return await context.Orders
                             .Include(o => o.Asset)
                             .ThenInclude(a => a.AssetType)
                             .Include(o => o.OrderState)
                             .ToListAsync();
    }


    public async Task<decimal> CalculateTotalAmountAsync(Order order)
    {
        var asset = await context.Assets
                                  .Include(a => a.AssetType)
                                  .FirstOrDefaultAsync(a => a.Id == order.AssetId);

        if (asset == null || asset.AssetType == null)
        {
            throw new InvalidOperationException("Asset or AssetType not found.");
        }

        decimal totalAmount = 0;
        string assetTypeDescription = asset.AssetType.Description.ToLowerInvariant();

        switch (assetTypeDescription)
        {
            case "accion":
                totalAmount = (asset.UnitPrice * order.Quantity) * 1.006m * 1.21m;
                break;

            case "bono":
                decimal bondPrice = order.Price!.Value;
                totalAmount = (bondPrice * order.Quantity) * 1.002m * 1.21m;
                break;

            case "fci":
                decimal fciPrice = order.Price!.Value;
                totalAmount = fciPrice * order.Quantity;
                break;

            default:
                throw new InvalidOperationException("Unknown asset type.");
        }

        return totalAmount;
    }


}