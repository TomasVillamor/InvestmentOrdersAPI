namespace InvestmentOrdersAPI.DataAccess.Models;

public class Order
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int AssetId { get; set; }
    public int OrderStateId { get; set; }
    public int Quantity { get; set; }
    public char Operation { get; set; }
    public decimal? Price { get; set; }
    public decimal TotalAmount { get; set; }
    public Asset Asset { get; set; } = default!;
    public OrderState OrderState { get; set; } = default!;
}



