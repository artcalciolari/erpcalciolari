namespace ErpCalciolari.DTOs.Read
{
    public class OrderReadDto
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemReadDto> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
