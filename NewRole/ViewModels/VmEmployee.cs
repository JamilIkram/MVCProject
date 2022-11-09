using NewRole.Models.DataBind;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewRole.ViewModels
{
    public class VmEmployee
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
        public string DesignationName { get; set; }
        public string DepertmentName { get; set; }
    }
}