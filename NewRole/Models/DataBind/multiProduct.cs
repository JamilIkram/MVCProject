namespace NewRole.Models.DataBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("multiProduct")]
    public partial class multiProduct
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public DateTime? SupplyDate { get; set; }

        public int Quantity { get; set; }

        public int? supplier_id { get; set; }

        public virtual supplier supplier { get; set; }
    }
}
