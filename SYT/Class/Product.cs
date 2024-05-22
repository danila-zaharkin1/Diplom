using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SYT.Class
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }

        [Required]
        public string Barcode { get; set; }

        [Required]
        public string Product_Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Связь с поставкой
        public Guid? Supply_Number { get; set; }
        [ForeignKey("Supply_Number")]
        public virtual Supply Supply { get; set; }
    }
}
