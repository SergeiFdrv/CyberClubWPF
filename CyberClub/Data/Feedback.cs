namespace CyberClub.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feedback")]
    public partial class Feedback
    {
        [Key]
        public int MessageID { get; set; }

        public int? Who { get; set; }

        [Required]
        [StringLength(50)]
        public string Briefly { get; set; }

        [StringLength(255)]
        public string InDetails { get; set; }

        public DateTime Dt { get; set; }

        public bool? IsRead { get; set; }

        public virtual User User { get; set; }
    }
}
