namespace InvestmentOrdersAPI.Dtos.Order;

public class OrderReadDto
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int AssetId { get; set; }
    public int OrderStateId { get; set; }
    public int Quantity { get; set; }
    public char Operation { get; set; }
    public decimal? Price { get; set; }
    public decimal TotalAmount { get; set; }
    public string AssetName { get; set; } = string.Empty;
    public string StateDescription { get; set; } = string.Empty;
}
