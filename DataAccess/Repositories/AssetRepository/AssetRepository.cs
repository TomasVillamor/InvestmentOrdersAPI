using investmentOrders.DataAccess;
using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.DataAccess.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrdersAPI.DataAccess.Repositories.AssetRepository;

public class AssetRepository : GenericRepository<Asset>, IAssetRepository
{
    public AssetRepository(DataAccessContext context) : base(context) { }

    public async Task<Asset> GetAssetByIdAsync(int id)
    {
        return await context.Assets.FindAsync(id);
    }
}