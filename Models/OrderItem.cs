using System.ComponentModel.DataAnnotations.Schema;

namespace ErpCalciolari.Models
{
    [Table("order_items")]
    public class OrderItem
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("order_number")]
        public int OrderNumber { get; set; } // Referência ao número do pedido

        [Column("product_code")]
        public int ProductCode { get; set; } // Chave estrangeira (identificador)

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal UnitPrice { get; set; }

        public Product Product { get; set; } // Propriedade de navegação

        public OrderItem(int orderNumber, int productCode, int quantity, decimal unitPrice)
        {
            OrderNumber = orderNumber;
            ProductCode = productCode;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
