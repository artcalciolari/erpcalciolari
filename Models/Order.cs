﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ErpCalciolari.Models
{
    [Table("orders")]
    public class Order
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("order_number")]
        public int OrderNumber { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; } // Chave estrangeira (identificador)

        [Column("order_date", TypeName = "timestamp with time zone")]
        public DateTime OrderDate { get; set; }

        [Column("delivery_date", TypeName = "timestamp with time zone")]
        public DateTime DeliveryDate { get; set; }

        public List<OrderItem> Items { get; set; }


        public Order(string customerName, int orderNumber, DateTime deliveryDate)
        {
            CustomerName = customerName;
            OrderNumber = orderNumber;
            OrderDate = DateTime.UtcNow;
            DeliveryDate = deliveryDate;
            Items = [];
        }
    }
}