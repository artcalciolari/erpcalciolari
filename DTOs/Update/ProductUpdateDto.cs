namespace ErpCalciolari.DTOs.Update
{
    public class ProductUpdateDto
    {
        public int? Code { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
