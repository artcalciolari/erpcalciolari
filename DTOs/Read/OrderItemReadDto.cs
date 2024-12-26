namespace ErpCalciolari.DTOs.Read
{
    public class OrderItemReadDto
    {
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
