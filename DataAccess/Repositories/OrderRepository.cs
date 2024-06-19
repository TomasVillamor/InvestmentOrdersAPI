using investmentOrders.DataAccess;
using InvestmentOrdersAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrdersAPI.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{

    private readonly DataAccessContext context;

    public OrderRepository(DataAccessContext context)
    {
        this.context = context;
    }
    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await context.Orders.ToListAsync();
    }
    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await context.Orders.FindAsync(id);
    }

    public async Task AddOrderAsync(Order order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();
    }
    public async Task UpdateOrderAsync(Order order)
    {
        context.Entry(order).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await context.Orders.FindAsync(id);
        if (order != null)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }
    }



}
