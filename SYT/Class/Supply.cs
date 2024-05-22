using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SYT.Class
{
    public class Supply
    {
        [Key]
        public Guid Supply_Number { get; set; }

        public DateTime SupplyDate { get; set; }
        public string Supplier { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
