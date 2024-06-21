using investmentOrders.DataAccess;
using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.DataAccess.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrdersAPI.DataAccess.Repositories.OrderStateRepository;

public class OrderStateRepository : GenericRepository<OrderState>, IOrderStateRepository
{
    private readonly DataAccessContext context;

    public OrderStateRepository(DataAccessContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<OrderState> GetOrderStateByDescriptionAsync(string description)
    {
        return await context.OrderStates.FirstOrDefaultAsync(s => s.Description == description);
    }

    public async Task<OrderState> GetOrderStateByIdAsync(int id)
    {
        return await context.OrderStates.FindAsync(id);
    }
}