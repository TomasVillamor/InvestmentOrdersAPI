using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.DataAccess.Repositories.GenericRepository;

namespace InvestmentOrdersAPI.DataAccess.Repositories.AssetRepository;

public interface IAssetRepository : IGenericRepository<Asset> { }