using ErpCalciolari.Models;

namespace ErpCalciolari.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> GetOrderWithOrderNumberAsync(int orderId);
        Task<Order> GetOrderWithIdAsync(Guid orderId);
        Task<List<Order>> GetOrdersAsync();
        Task<bool> UpdateOrderAsync(Guid id, Order order);
        Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
