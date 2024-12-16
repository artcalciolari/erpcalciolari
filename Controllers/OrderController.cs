using ErpCalciolari.DTOs.Create;
using ErpCalciolari.DTOs.Update;
using ErpCalciolari.Services;
using Microsoft.AspNetCore.Mvc;

namespace ErpCalciolari.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;

        public OrderController(OrderService orderService)
        {
            _service = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto createDto)
        {
            var order = await _service.CreateOrderAsync(createDto);
            return CreatedAtAction(nameof(GetOrderWithId), new { id = order.Id }, order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _service.GetOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderWithId(Guid id)
        {
            var order = await _service.GetOrderWithIdAsync(id);
            return Ok(order);
        }

        [HttpGet("orderNumber/{orderNumber}")]
        public async Task<IActionResult> GetOrderWithNumber(int orderNumber)
        {
            var order = await _service.GetOrderWithOrderNumberAsync(orderNumber);
            return Ok(order);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderUpdateDto updateDto)
        {
            var updatedOrder = await _service.UpdateOrderAsync(id, updateDto);
            return Ok(updatedOrder);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _service.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
