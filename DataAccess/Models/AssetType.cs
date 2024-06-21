namespace InvestmentOrdersAPI.DataAccess.Models;

public class AssetType
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public ICollection<Asset> Assets { get; set; } = default!;
}
