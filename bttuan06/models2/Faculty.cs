namespace bttuan06.models2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Faculty")]
    public partial class Faculty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FacultyID { get; set; }

        [Required]
        [StringLength(50)]
        public string FacultyName { get; set; }
    }
}
