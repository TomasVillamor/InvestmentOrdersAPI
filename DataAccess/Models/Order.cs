namespace InvestmentOrdersAPI.DataAccess.Models;

public class Order
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string AssetName { get; set; } = string.Empty;
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public char Operation { get; set; }
    public int? State { get; set; }
    public decimal? TotalAmount { get; set; }
}



