//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SYT.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products
    {
        public System.Guid Product_ID { get; set; }
        public string Product_Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.Guid> Supply_Number { get; set; }
        public string Barcode { get; set; }
    
        public virtual Supplies Supplies { get; set; }
    }
}