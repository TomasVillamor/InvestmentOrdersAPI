namespace InvestmentOrdersAPI.DataAccess.Models;

public class OrderState
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public ICollection<Order> Orders { get; set; } = default!;
}
