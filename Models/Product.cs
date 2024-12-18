using System.ComponentModel.DataAnnotations.Schema;

namespace ErpCalciolari.Models
{
    [Table("products")]
    public class Product
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("code")]
        public int Code { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }


        public Product(int code, string name, string type, int quantity, decimal price)
        {
            Id = Guid.NewGuid();
            Code = code;
            Name = name;
            Type = type;
            Quantity = quantity;
            Price = price;
        }
    }
}
