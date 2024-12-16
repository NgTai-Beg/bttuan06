namespace bttuan06.models2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public string StudentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Fullname { get; set; }

        public double AverageScore { get; set; }

        public int FacultyID { get; set; }
        [ForeignKey("FacultyID")]
        public virtual Faculty Faculty { get; set; }
    }
}
