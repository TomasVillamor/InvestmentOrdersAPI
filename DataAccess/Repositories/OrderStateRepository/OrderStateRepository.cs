using investmentOrders.DataAccess;
using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.DataAccess.Repositories.GenericRepository;

namespace InvestmentOrdersAPI.DataAccess.Repositories.OrderStateRepository;

public class OrderStateRepository : GenericRepository<OrderState>, IOrderStateRepository
{

    public OrderStateRepository(DataAccessContext context) : base(context) { }

    public async Task<OrderState> GetOrderStateByDescriptionAsync(string description)
    {
        return await context.OrderStates.FirstOrDefaultAsync(s => s.Description == description);
    }

}