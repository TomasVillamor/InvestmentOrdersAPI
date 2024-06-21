namespace InvestmentOrdersAPI.DataAccess.Models;

public class Asset
{
    public int Id { get; set; }
    public int AssetTypeId { get; set; }
    public string Ticker { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public AssetType AssetType { get; set; } = default!;
    public ICollection<Order> Orders { get; set; } = default!;
}
