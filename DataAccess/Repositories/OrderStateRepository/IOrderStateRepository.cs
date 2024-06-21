using InvestmentOrdersAPI.DataAccess.Models;
using InvestmentOrdersAPI.DataAccess.Repositories.GenericRepository;

namespace InvestmentOrdersAPI.DataAccess.Repositories.OrderStateRepository;

public interface IOrderStateRepository : IGenericRepository<OrderState>
{
    Task<OrderState> GetOrderStateByDescriptionAsync(string description);
    Task<OrderState> GetOrderStateByIdAsync(int id);
}