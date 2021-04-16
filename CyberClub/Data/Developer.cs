namespace CyberClub.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Developer
    {
        public Developer()
        {
            Games = new HashSet<Game>();
        }

        public int DeveloperID { get; set; }

        [Required]
        [StringLength(50)]
        public string DeveloperName { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
