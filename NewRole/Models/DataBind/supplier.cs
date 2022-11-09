namespace NewRole.Models.DataBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("supplier")]
    public partial class supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public supplier()
        {
            multiProducts = new HashSet<multiProduct>();
        }

        [Key]
        public int supplier_id { get; set; }

        public string supplier_name { get; set; }

        public decimal? mobile { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? JoinDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<multiProduct> multiProducts { get; set; }
    }
}
