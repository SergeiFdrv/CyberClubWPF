namespace CyberClub.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Genre
    {
        public Genre()
        {
            Games = new HashSet<Game>();
        }

        public int GenreID { get; set; }

        [Required]
        [StringLength(30)]
        public string GenreName { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
