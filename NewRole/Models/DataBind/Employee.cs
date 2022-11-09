namespace NewRole.Models.DataBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        public int EmployeeId { get; set; }

        [StringLength(50)]
        public string EmployeeName { get; set; }

        [StringLength(600)]
        public string ImagePath { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int Age { get; set; }

        public DateTime JoinningDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Gender { get; set; }

        public int DepertmentId { get; set; }

        public int DesignationId { get; set; }

        public virtual Depertment Depertment { get; set; }

        public virtual Designation Designation { get; set; }
    }
}
