namespace NewRole.Models.DataBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Traineertblvm")]
    public partial class Traineertblvm
    {
        [Key]
        public int TraineerId { get; set; }

        [Required]
        [StringLength(50)]
        public string TraineerName { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Contact { get; set; }

        public DateTime TraineerDOB { get; set; }

        [StringLength(600)]
        public string ImageName { get; set; }

        [StringLength(600)]
        public string ImageUrl { get; set; }
    }
}
