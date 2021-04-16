namespace CyberClub.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        public Image()
        {
            Games = new HashSet<Game>();
        }

        public int ImageID { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageName { get; set; }

        [Required]
        public byte[] Bin { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
