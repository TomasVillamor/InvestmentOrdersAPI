using investmentOrders.DataAccess;
using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.DataAccess.Repositories.GenericRepository;

namespace InvestmentOrdersAPI.DataAccess.Repositories.AssetRepository;

public class AssetRepository : GenericRepository<Asset>, IAssetRepository
{
    public AssetRepository(DataAccessContext context) : base(context) { }
}