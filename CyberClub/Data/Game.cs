namespace CyberClub.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game
    {
        public Game()
        {
            Subscriptions = new HashSet<Subscription>();
            Genres = new HashSet<Genre>();
        }

        public int GameID { get; set; }

        [Required]
        [StringLength(50)]
        public string GameName { get; set; }

        [Column("Developer")]
        public int? DeveloperID { get; set; }

        [StringLength(255)]
        public string GameExePath { get; set; }

        public int? GameIcon { get; set; }

        public bool? IsSingleplayer { get; set; }

        public bool? IsMultiplayer { get; set; }

        public virtual Developer Developer { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
    }
}
