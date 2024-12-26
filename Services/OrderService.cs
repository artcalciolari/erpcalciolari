using ErpCalciolari.DTOs.Create;
using ErpCalciolari.DTOs.Read;
using ErpCalciolari.DTOs.Update;
using ErpCalciolari.Models;
using ErpCalciolari.Repositories;
using ErpCalciolari.Repositories.Interfaces;

namespace ErpCalciolari.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductionRequirementRepository _requirementRepository;
        private readonly CustomerService _customerService;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IProductionRequirementRepository requirementRepository, CustomerService customerService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _requirementRepository = requirementRepository;
            _customerService = customerService;
        }

        public async Task<Order> CreateOrderAsync(OrderCreateDto createDto)
        {
            var order = new Order(createDto.CustomerName, createDto.OrderNumber, createDto.DeliveryDate, createDto.Status);

            if (!await _customerService.CustomerExistsWithName(createDto.CustomerName))
            {
                throw new KeyNotFoundException("Customer not found. Ensure the customer exists before creating an order.");
            }

            foreach (var item in createDto.Items)
            {
                var product = await _productRepository.GetProductWithCodeAsync(item.ProductCode);

                if (product.Quantity < item.Quantity)
                {
                    await HandleProductionRequirementAsync(item, product, order);
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
                OrderDate = o.OrderDate,
                DeliveryDate = o.DeliveryDate,
                Status = o.Status,
                Items = o.Items.Select(i => new OrderItemReadDto
                {
                    ProductCode = i.ProductCode,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
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
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                Status = order.Status,
                Items = order.Items.Select(i => new OrderItemReadDto
                {
                    ProductCode = i.ProductCode,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
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
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                Status = order.Status,
                Items = order.Items.Select(i => new OrderItemReadDto
                {
                    ProductCode = i.ProductCode,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
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
                        await HandleProductionRequirementAsync(newItem, product, order);
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

        private async Task HandleProductionRequirementAsync(OrderItemCreateDto item, Product product, Order order)
        {
            int additionalProductionNeeded = item.Quantity - product.Quantity;

            product.NeedsProduction = true;

            // Adiciona ou atualiza o registro na tabela de produção
            var requirement = await _requirementRepository.GetProductionRequirementWithProductCodeAsync(product.Code);
            if (requirement == null)
            {
                await _requirementRepository.CreateProductionRequirementAsync(
                    new ProductionRequirement(product.Code, additionalProductionNeeded, order.DeliveryDate));
            }
            else
            {
                requirement.RequiredQuantity += additionalProductionNeeded;
                await _requirementRepository.UpdateProductionRequirementAsync(requirement);
            }

            await _productRepository.UpdateProductAsync(product.Id, product);
        }
    }
}
