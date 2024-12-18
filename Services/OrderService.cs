using ErpCalciolari.DTOs.Create;
using ErpCalciolari.DTOs.Read;
using ErpCalciolari.DTOs.Update;
using ErpCalciolari.Models;
using ErpCalciolari.Repositories.Interfaces;

namespace ErpCalciolari.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository producutRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = producutRepository;
        }

        public async Task<Order> CreateOrderAsync(OrderCreateDto createDto)
        {
            var order = new Order(createDto.CustomerName, createDto.OrderNumber, createDto.DeliveryDate);

            foreach (var item in createDto.Items)
            {
                var product = await _productRepository.GetProductWithCodeAsync(item.ProductCode);

                if (product.Quantity < item.Quantity)
                {
                    throw new InvalidOperationException($"{product.Name} has only {product.Quantity}g available.");
                }

                product.Quantity -= item.Quantity; // Atualiza o estoque

                decimal totalPrice = (product.Price * item.Quantity) / 1000;

                order.Items.Add(new OrderItem(order.OrderNumber, item.ProductCode, item.Quantity, totalPrice));
            }

            await _orderRepository.CreateOrderAsync(order);
            return order;
        }

        public async Task<List<OrderReadDto>> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return orders.Select(o => new OrderReadDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                CustomerName = o.CustomerName,
                Items = o.Items.Select(i => new OrderItemReadDto
                {
                    ProductCode = i.ProductCode,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                DeliveryDate = o.DeliveryDate
            }).ToList();
        }

        public async Task<OrderReadDto> GetOrderWithIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderWithIdAsync(orderId);
            return new OrderReadDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                CustomerName = order.CustomerName,
                Items = order.Items.Select(i => new OrderItemReadDto
                {
                    ProductCode = i.ProductCode,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                DeliveryDate = order.DeliveryDate
            };
        }

        public async Task<OrderReadDto> GetOrderWithOrderNumberAsync(int orderNumber)
        {
            var order = await _orderRepository.GetOrderWithOrderNumberAsync(orderNumber);
            return new OrderReadDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                CustomerName = order.CustomerName,
                Items = order.Items.Select(i => new OrderItemReadDto
                {
                    ProductCode = i.ProductCode,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                DeliveryDate = order.DeliveryDate
            };
        }

        public async Task<bool> UpdateOrderAsync(Guid id, OrderUpdateDto updateDto)
        {
            var order = await _orderRepository.GetOrderWithIdAsync(id);

            // Atualiza o nome do cliente caso presente
            if (!string.IsNullOrEmpty(updateDto.CustomerName))
            {
                order.CustomerName = updateDto.CustomerName;
            }

            // Atualiza a data de entrega caso presente
            if (updateDto.DeliveryDate.HasValue)
            {
                order.DeliveryDate = updateDto.DeliveryDate.Value;
            }

            // Atualiza os itens do pedido caso presente
            if (updateDto.Items != null)
            {
                // Restaura a quantidade dos produtos
                foreach (var oldItem in order.Items)
                {
                    var product = await _productRepository.GetProductWithCodeAsync(oldItem.ProductCode);
                    product.Quantity += oldItem.Quantity;
                }

                // Limpa os itens antigos do pedido
                order.Items.Clear();

                // Adiciona os novos itens
                foreach (var newItem in updateDto.Items)
                {
                    var product = await _productRepository.GetProductWithCodeAsync(newItem.ProductCode);

                    if (product.Quantity < newItem.Quantity)
                    {
                        throw new InvalidOperationException($"{product.Name} has only {product.Quantity}g available.");
                    }

                    product.Quantity -= newItem.Quantity; // Atualiza o estoque

                    decimal totalPrice = (product.Price * newItem.Quantity) / 1000;

                    order.Items.Add(new OrderItem(order.OrderNumber, newItem.ProductCode, newItem.Quantity, totalPrice));
                }
            }
            return await _orderRepository.UpdateOrderAsync(id, order);
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }
    }
}
