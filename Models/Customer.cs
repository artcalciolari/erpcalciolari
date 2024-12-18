using System.ComponentModel.DataAnnotations.Schema;

namespace ErpCalciolari.Models
{
    [Table("customers")]
    public class Customer
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("name")]
        public string Name { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("address")]
        public string? Address { get; set; }


        public Customer(string name, string phone, string? email, string? address)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}
