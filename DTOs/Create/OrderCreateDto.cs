namespace ErpCalciolari.DTOs.Create
{
    public class OrderCreateDto
    {
        public string CustomerName { get; set; }
        public int OrderNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public List<OrderItemCreateDto> Items { get; set; }
    }
}
