using ErpCalciolari.Infra;
using ErpCalciolari.Models;
using ErpCalciolari.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ErpCalciolari.Repositories
{
    /// TODO: arrumar os DTOS de pedidos, produtos e ver porque raios o dto de pedidos está retornando a data do pedido como o valor padrão (mesmo estando certo no banco)
    public class OrderRepository : IOrderRepository
    {
        private readonly MyDbContext _context;

        public OrderRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .ToListAsync()
                ?? throw new Exception($"No orders found.");
        }

        public async Task<Order> GetOrderWithIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new Exception($"Order with ID {orderId} not found.");
        }

        public async Task<Order> GetOrderWithOrderNumberAsync(int orderNumber)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber)
                ?? throw new Exception($"Order with number {orderNumber} not found.");
        }

        public async Task<bool> UpdateOrderAsync(Guid id, Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await GetOrderWithIdAsync(orderId);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
