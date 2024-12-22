using System.ComponentModel.DataAnnotations.Schema;

namespace ErpCalciolari.Models
{
    [Table("production_requirements")]
    public class ProductionRequirement
    {

        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("product_code")]
        public int ProductCode { get; set; } // Chave estrangeira (identificador)

        [Column("required_quantity")]
        public int RequiredQuantity { get; set; }

        [Column("required_date")]
        public DateTime RequiredDate { get; set; }

        public Product Product { get; set; } // Propriedade de navegação

        public ProductionRequirement(int productCode, int requiredQuantity, DateTime requiredDate)
        {
            ProductCode = productCode;
            RequiredQuantity = requiredQuantity;
            RequiredDate = requiredDate;
        }
    }
}
