using ErpCalciolari.DTOs.Create;

namespace ErpCalciolari.DTOs.Update
{
    public class OrderUpdateDto
    {
        public string? CustomerName { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? Status { get; set; }
        public List<OrderItemCreateDto>? Items { get; set; }
    }
}
