using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.DataAccess.Repositories.GenericRepository;

namespace InvestmentOrdersAPI.DataAccess.Repositories.OrderRepository.OrderRepository;
public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetAllOrdersWithRelationsAsync();
    Task<decimal> CalculateTotalAmountAsync(Order order);
}