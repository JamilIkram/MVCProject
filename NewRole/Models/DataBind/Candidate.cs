namespace NewRole.Models.DataBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Candidate")]
    public partial class Candidate
    {
        public int CandidateId { get; set; }

        [StringLength(50)]
        public string CandidateName { get; set; }

        public decimal Salary { get; set; }

        [StringLength(600)]
        public string ImagePath { get; set; }

        public DateTime? Applydate { get; set; }

        public int Target { get; set; }

        public int AreaId { get; set; }
    }
}
